using LightBDD.NUnit2;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TUI.Places.Source;
using TUI.Sandbox.Controllers;

namespace TUI.Sandbox.Test.Source.Scenarios
{
    public partial class CityControllerFeatures: FeatureFixture
    {
        private void An_empty_city_List()
        {
            using (var session = this._cityUnit.GetSession())
            {
                //Arrange
                var cityRepo = session.GetRepository();

                // Act
                cityRepo.RemoveRange(cityRepo.GetAll());

                // Assert
                Assert.AreEqual(0, cityRepo.GetAll().Count());
            }
        }

        private void Add_element_from_create_page()
        {
            //Arrange
            this._model = CityFactory.GetCity(CityType.Paris);

            // Act
            this._controller.Create(this._model);
        }

        private void Check_details_of_the_added_city()
        {
            //Arrange

            // Act
            ViewResult result = this._controller.Details(this._model.Id) as ViewResult;
            var detailsCity = result.Model as City;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(detailsCity);
            Assert.AreEqual(detailsCity.Name, this._model.Name);
            Assert.AreEqual(detailsCity.Location.Description, this._model.Location.Description);
            Assert.AreEqual("Details",result.ViewName);
        }

        private void Check_details_of_the_undefined_city()
        {
            //Arrange

            // Act
            RedirectToRouteResult routeResult = this._controller.Details(1) as RedirectToRouteResult;
            var message = routeResult.RouteValues["Notification"];

            // Assert
            Assert.IsNotNull(message);
            Assert.AreEqual("notification", routeResult.RouteValues["controller"].ToString().ToLower());
        }

        private void Check_list_page_has_two_cities()
        {
            //Arrange

            // Act
            ViewResult result = this._controller.Index() as ViewResult;
            var cities = result.Model as IEnumerable<City>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(cities);
            Assert.AreEqual(2, cities.Count());
            Assert.AreEqual("Index", result.ViewName);
        }

        private void Delete_first_element()
        {
            using (var session = this._cityUnit.GetSession())
            {
                var cityRepo = session.GetRepository();

                var cts = cityRepo.GetAll();
                cityRepo.Remove(cts.First());

                session.Complete();
            }
        }

        private void Check_list_page_has_one_city()
        {
            //Arrange

            // Act
            ViewResult result = this._controller.Index() as ViewResult;
            var cities = result.Model as IEnumerable<City>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(cities);
            Assert.AreEqual(1, cities.Count());
            Assert.AreEqual("Index", result.ViewName);
        }

        private void Edit_added_element()
        {
            //Arrange

            // Act
            this._model.Name = CityFactory.GetCity(CityType.NewYork).Name;
            this._controller.Edit(this._model);
        }

        private void Check_details_of_the_edited_city()
        {
            //Arrange

            // Act
            ViewResult result = this._controller.Details(this._model.Id) as ViewResult;
            var detailsCity = result.Model as City;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(detailsCity);
            Assert.AreEqual(detailsCity.Name, CityFactory.GetCity(CityType.NewYork).Name);
            Assert.AreEqual(detailsCity.Location.Description, this._model.Location.Description);
            Assert.AreEqual("Details", result.ViewName);
        }

    }
}
