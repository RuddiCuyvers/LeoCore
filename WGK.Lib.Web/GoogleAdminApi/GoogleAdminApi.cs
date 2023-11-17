using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace WGK.Lib.Web.GoogleAdminApi
{
   public class GoogleAdminApi
    {
    
        #region Google API inloggen en aanspreken
        public static DirectoryService GoogleInloggen(string Scope)
        {
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string path = ((directory + @"\GoogleAdminApi\cert.p12").Remove(0, 6));

            //Service Account email adres
           const  String serviceAccountEmail = "lms-service@wgk-lms.iam.gserviceaccount.com";
            //Certificaat in var steken
            var certificate = new X509Certificate2(path, "notasecret", X509KeyStorageFlags.Exportable);
            // Service account gaat volgend account zijn rechten aannemen / voordoen
            ServiceAccountCredential credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = new[] { Scope },
                User = "frank.joriskes@limburg.wgk.be",

            }.FromCertificate(certificate));
            // Service aanmaken
            var service = new DirectoryService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "name of your app",
            });
            return service;



        }
        #endregion

        public static List<String> GoogleGroepenOphalen(string email)
        {
            DirectoryService _service = GoogleInloggen(DirectoryService.Scope.AdminDirectoryGroup);
            GroupsResource.ListRequest insertRequest = _service.Groups.List();

            insertRequest.UserKey = email;
            try
            {
                Google.Apis.Admin.Directory.directory_v1.Data.Groups creategrp = insertRequest.Execute();
                var groepen = new List<string>();

                try
                {
                    if (creategrp.GroupsValue != null)
                    {
                        foreach (var item in creategrp.GroupsValue)
                        {


                            groepen.Add(item.Name);
                            groepen.Add(item.Kind);




                        }
                    }
                    groepen.Add("Geengroep");

                }
                catch (Exception)
                {
                    groepen.Add("Geengroep");
                    throw;
                }

                return groepen;




            }
            catch (Google.GoogleApiException ex)
            {
                throw ex;
                return null;
            }
          
        }
public static string GoogleFotoOphalen(string clientId)
        {
            //Ingeven van welke user er gegevens opgevraagd worden
            var client = GoogleInloggen(DirectoryService.Scope.AdminDirectoryUser).Users.Get(clientId);
            //Uitvoeren van de request en in User OBJ steken
            User user = client.Execute();

            if (user.ThumbnailPhotoUrl != null)
            {
                return user.ThumbnailPhotoUrl;
            }
            return "";
          
        }

        public static string GoogleNaam(string clientId)
        {
            //Ingeven van welke user er gegevens opgevraagd worden
            var client = GoogleInloggen(DirectoryService.Scope.AdminDirectoryUser).Users.Get(clientId);
            //Uitvoeren van de request en in User OBJ steken
            User user = client.Execute();


            //Display(yara.);
            return user.Name.GivenName + " " + user.Name.FamilyName;

        }
        public static string GooglePN(string clientId)
        {
            //Ingeven van welke user er gegevens opgevraagd worden
            var client = GoogleInloggen(DirectoryService.Scope.AdminDirectoryUser).Users.Get(clientId);
            //Uitvoeren van de request en in User OBJ steken
            User user = client.Execute();
            var nummers = "";
            try
            {
                if (user.Phones != null)
                {
                    foreach (var item in user.Phones)
                    {
                        nummers += item.Value + "\n";
                    }
                    //Display(yara.);
                    return nummers;
                }
                return "Geen telefoon nummer bekend";

            }  
            catch (Exception)
            {

                return "Geen telefoon nummer bekend";
            }
           

        }
        public static string GoogleOR(string clientId)
        {
            //Ingeven van welke user er gegevens opgevraagd worden
            var client = GoogleInloggen(DirectoryService.Scope.AdminDirectoryUser).Users.Get(clientId);
            //Uitvoeren van de request en in User OBJ steken
            User user = client.Execute();


            if (user.Organizations != null)
            {

                foreach (var item in user.Organizations)
                {
                    if (item.Department != null)
                    {
                        return item.Department;
                    }
                    else if (item.Location != null)
                    {
                        return item.Location;
                    }
                    return "Geen info bekend";
                }

            }

            return "Geen info bekend";
        }
        public static string GoogleON(string clientId)
        {
            //Ingeven van welke user er gegevens opgevraagd worden
            var client = GoogleInloggen(DirectoryService.Scope.AdminDirectoryUser).Users.Get(clientId);
            //Uitvoeren van de request en in User OBJ steken
            User user = client.Execute();
        

            if (user.Organizations != null)
            {

                foreach (var item in user.Organizations)
                {
                    if (item.Name != null)
                    {
                        return item.Name;
                    }
                   
                    else
                    {
                        return "Geen correcte info bekend";
                    }
                
                }
                
            }

            return "Geen info bekend";
        }
        public static string GoogleOT(string clientId)
        {
            //Ingeven van welke user er gegevens opgevraagd worden
            var client = GoogleInloggen(DirectoryService.Scope.AdminDirectoryUser).Users.Get(clientId);
            //Uitvoeren van de request en in User OBJ steken
            User user = client.Execute();


            if (user.Organizations != null)
            {

                foreach (var item in user.Organizations)
                {
                    if (item.Title != null)
                    {
                        return item.Title;
                    }
                   
                    return "Geen info bekend";
                }

            }

            return "Geen info bekend";
        }
        public static IList GoogleAfdelingOphalen(string clientId)
        {
            //Ingeven van welke user er gegevens opgevraagd worden
            var client = GoogleInloggen(DirectoryService.Scope.AdminDirectoryUser).Users.Get(clientId);
            //Uitvoeren van de request en in User OBJ steken
            User user = client.Execute();
            return (IList)user.Organizations;
        }

    }
}
