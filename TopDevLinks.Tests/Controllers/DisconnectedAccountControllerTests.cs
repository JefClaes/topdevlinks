using System.Web.Mvc;
using NUnit.Framework;
using TopDevLinks.Controllers;
using TopDevLinks.Models.Entities;
using TopDevLinks.Models.ViewModels;

namespace TopDevLinks.Tests.Controllers
{
    // TODO: fix this!
    //[TestFixture]
    //public class DisconnectedAccountControllerTests : DisconnectedControllerTestFixture<AccountController>
    //{
    //    protected override AccountController CreateController()
    //    {
    //        return new AccountController();
    //    }

    //    [Test]
    //    public void Post_Login_no_return_url_and_valid_credentials_redirects_to_home_index()
    //    {
    //        var user = new User("davybrion", "ralinx@davybrion.com");
    //        user.SetPassword("blabla");
    //        AddQueryResponse(user);

    //        var result = Controller.Login(new LoginViewModel { UserName = "davybrion", Password = "blabla" }, null);
    //        var redirect = (RedirectToRouteResult)result;

    //        Assert.AreEqual("/Home/Index", redirect.RouteName);
    //    }
    //}
}