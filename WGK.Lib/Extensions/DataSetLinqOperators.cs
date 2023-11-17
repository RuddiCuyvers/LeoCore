// This code has been provided by Andrew Conrad from Microsoft
// See http://blogs.msdn.com/aconrad/archive/2007/09/07/science-project.aspx

//A few notes about this code:
//1.  The initial schema of the DataTable is based on schema of the type T.  All public property and fields are turned into DataColumns.
//2.  If the source sequence contains a sub-type of T, the table is automatically expanded for any addition public properties or fields.
//3.  If you want to provide a existing table, that is fine as long as the schema is consistent with the schema of the type T.
//4.  Obviously this sample probably needs some perf work.  Feel free to suggest improvements.

//UPDATE 9/14 - Based on some feedback from akula, I have fixed a couple of issues with the code:
//1) The code now supports loading sequences of scalar values.
//2) Cases where the developer provides a datatable which needs to be completely extended based on the type T is now supported.
//UPDATE 12/17 - In the comments, Nick Lucas has provided a solution to handling Nullable types in the input sequence.  I have not tried it yet, but it look like it works.


// Sample usage:

//// 1 - create sequence 
//Item[] items = new Item[] { new Book{Id = 1, Price = 13.50, Genre = "Comedy", Author = "Jim Bob"}, 
//                            new Book{Id = 2, Price = 8.50, Genre = "Drama", Author = "John Fox"},  
//                            new Movie{Id = 1, Price = 22.99, Genre = "Comedy", Director = "Phil Funk"},
//                            new Movie{Id = 1, Price = 13.40, Genre = "Action", Director = "Eddie Jones"}};


//var query1 = from i in items
//             where i.Price > 9.99
//             orderby i.Price
//             select i;

//// load into new DataTable
//DataTable table1 = query1.CopyToDataTable();

//// 2 - load into existing DataTable - schemas match            
//DataTable table2 = new DataTable();
//table2.Columns.Add("Price", typeof(int));
//table2.Columns.Add("Genre", typeof(string));

//var query2 = from i in items
//             where i.Price > 9.99
//             orderby i.Price
//             select new {i.Price, i.Genre};

//query2.CopyToDataTable(table2, LoadOption.PreserveChanges);


//// 3 - load into existing DataTable - expand schema + autogenerate new Id.
//DataTable table3 = new DataTable();
//DataColumn dc = table3.Columns.Add("NewId", typeof(int));
//dc.AutoIncrement = true;
//table3.Columns.Add("ExtraColumn", typeof(string));

//var query3 = from i in items
//             where i.Price > 9.99
//             orderby i.Price
//             select new { i.Price, i.Genre };

//query3.CopyToDataTable(table3, LoadOption.PreserveChanges);

//// 4 - load sequence of scalars.

//var query4 = from i in items
//             where i.Price > 9.99
//             orderby i.Price
//             select i.Price;

//var DataTable4 = query4.CopyToDataTable();


using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace WGK.Lib.Extensions
{
    public static class DataSetLinqOperators
    {
        /// <summary>
        /// Creates a <see cref="DataTable"/> that contains the data from a source sequence.
        /// </summary>
        /// <remarks>
        /// The initial schema of the DataTable is based on schema of the type T. All public property and fields are turned into DataColumns.
        ///  If the source sequence contains a sub-type of T, the table is automatically expanded for any addition public properties or fields.
        /// </remarks>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        /// <summary>
        /// Loads the data from a source sequence into an existing <see cref="DataTable"/>.
        /// </summary>
        /// <remarks>
        /// The schema of <paramref name="table" /> must be consistent with the schema of the type T (all public property and fields are mapped to DataColumns).
        /// If the source sequence contains a sub-type of T, the table is automatically expanded for any addition public properties or fields.
        /// </remarks>
        public static DataTable LoadSequence<T>(this IEnumerable<T> source,
          DataTable table, LoadOption? options)
        {
            if (table == null)
                throw new ArgumentNullException("table");
            return new ObjectShredder<T>().Shred(source, table, options);
        }
    }

    #region ObjectShredder class
    /// <summary>
    /// Helper class used by DataSetLinqOperators
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectShredder<T>
    {
        private FieldInfo[] _fi;
        private PropertyInfo[] _pi;
        private Dictionary<string, int> _ordinalMap;
        private Type _type;

        public ObjectShredder()
        {
            _type = typeof(T);
            _fi = _type.GetFields();
            _pi = _type.GetProperties();
            _ordinalMap = new Dictionary<string, int>();
        }

        public DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            if (typeof(T).IsPrimitive)
            {
                return ShredPrimitive(source, table, options);
            }

            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            // now see if need to extend datatable base on the type T + build ordinal map
            table = ExtendTable(table, typeof(T));

            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (options != null)
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), true);
                    }
                }
            }
            table.EndLoadData();
            return table;
        }

        public DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            if (!table.Columns.Contains("Value"))
            {
                table.Columns.Add("Value", typeof(T));
            }

            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                Object[] values = new Object[table.Columns.Count];
                while (e.MoveNext())
                {
                    values[table.Columns["Value"].Ordinal] = e.Current;

                    if (options != null)
                    {
                        table.LoadDataRow(values, (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(values, true);
                    }
                }
            }
            table.EndLoadData();
            return table;
        }

        public DataTable ExtendTable(DataTable table, Type type)
        {
            // value is type derived from T, may need to extend table.
            foreach (FieldInfo f in type.GetFields())
            {
                if (!_ordinalMap.ContainsKey(f.Name))
                {
                    DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                                  : table.Columns.Add(f.Name, f.FieldType);
                    _ordinalMap.Add(f.Name, dc.Ordinal);
                }
            }
            foreach (PropertyInfo p in type.GetProperties())
            {
                if (!_ordinalMap.ContainsKey(p.Name))
                {
                    DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                                  : table.Columns.Add(p.Name, p.PropertyType);
                    _ordinalMap.Add(p.Name, dc.Ordinal);
                }
            }
            return table;
        }

        public object[] ShredObject(DataTable table, T instance)
        {
            FieldInfo[] fi = _fi;
            PropertyInfo[] pi = _pi;

            if (instance.GetType() != typeof(T))
            {
                ExtendTable(table, instance.GetType());
                fi = instance.GetType().GetFields();
                pi = instance.GetType().GetProperties();
            }

            Object[] values = new object[table.Columns.Count];
            foreach (FieldInfo f in fi)
            {
                values[_ordinalMap[f.Name]] = f.GetValue(instance);
            }

            foreach (PropertyInfo p in pi)
            {
                values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
            }
            return values;
        }
    }
    #endregion
}