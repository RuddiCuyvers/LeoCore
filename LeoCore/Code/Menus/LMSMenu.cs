

using LEO.Common.Constants.Trainings;
using LEO.Common.Literals;

using LeoCore.Controllers;

using WGK.Lib.Web.Mvc.Controls.Menu;
using System.Linq;
using LEO.Common.Constants.IBF;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeoCore.Code.Menus
{
    public class LMSMenu : IMenuConfig
    {
        public Microsoft.AspNetCore.Html.HtmlString Render(IHtmlHelper pHtmlHelper)
        {
            return new MenuBuilder(pHtmlHelper)
                .Items(pMenu =>
                {

                    //pMenu.Add()
                    //.Text("Home")
                    //.Action<HomeController>(p => p.index());

                    //1
                    pMenu.Add()
                          .Text(string.Format("Les zoeken"))
                          .Action<IBFController>(p => p.Identification("","2023",""));
                    //pMenu.Add()
                    //        .Text(string.Format(CommonLiterals.IdentificationPageTitle, TRAININGDisplayNames.cTrainingEntityDisplayName))
                    //      .Action<IBFController>(p => p.Identification("", "2023", "")); ;

                      //pMenu.Add()
                      //            .Text(string.Format(CommonLiterals.MaintenancePageCreateTitle, TRAININGDisplayNames.cTrainingEntityDisplayName))
                      //             .Action<IBFController>(p => p.Identification("", "2023", ""));

                    pMenu.Add()
                          .Text(string.Format("les maken"))
                         .Action<TrainingsController>(p => p.Maintenance(2));

                    pMenu.Add()
                          .Text(string.Format("les maken"))
                         .Action<TrainingsController>(p => p.Identification());

                    // pMenu.Add()
                    //.Text("Contact")
                    //.Action<HomeController>(p => p.contact());


                })
                .ToHtmlString();
        }
    }
}