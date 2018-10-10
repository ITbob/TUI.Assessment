using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;
using TUI.TimeZone.Source;
using TUI.TimeZone.Source.Api.Google;
using Moq;

namespace TUI.TimeZone.Test.Source
{
    [TestFixture]
    class TimeZoneHelperTest
    {
        private DateTime _refDate;
        private Location _calcutta;
        private Location _paris;

        [OneTimeSetUp]
        public void SetUp()
        {
            this._refDate = new DateTime(2018, 09, 27, 13, 20, 0);
            this._calcutta = new Location()
            {
                Latitude = 22.6520429,
                Longitude = 88.4463299
            };

            this._paris = new Location()
            {
                Latitude = 49.0096906,
                Longitude = 2.547924500000022
            };
        }

        [TestCase(49.0096906, 2.547924500000022, "9/27/2018 3:20:00 PM")]
        [TestCase(22.6520429, 88.4463299, "9/27/2018 6:50:00 PM")]
        public void Should_Get_Local_Time_When_Using_Google_Api(
            Double latitude,
            Double longitude,
            String expectedResult)
        {
            var api = new GoogleTimeZoneApi();
            var date = new DateTime(2018, 09, 27, 13, 20, 0);
            var result = DateTime.MinValue;
            var location = new Location()
            {
                Latitude = latitude,
                Longitude = longitude
            };

            var succeed = api.GetLocalTime(location,date, ref result);

            if (!succeed)
            {
                Assert.Fail();
            }
            else
            {
                Assert.AreEqual(Convert.ToDateTime(expectedResult), result);
            }
        }

        delegate void LocalTimeCallBack(Location location, DateTime datetime, ref DateTime amount);

        [Test]
        public void Should_Be_3Hours_and_30minutes_between_Paris_and_Calcutta_When_Using_Mock()
        {
            var mock = new Mock<ITimeZoneApi>();
            mock.Setup(v => v.GetLocalTime(
                    this._calcutta,
                    this._refDate, 
                    ref It.Ref<DateTime>.IsAny))
                .Callback(new LocalTimeCallBack(
                    (Location val, DateTime dd, ref DateTime rr) =>
                    {
                        rr = new DateTime(2018, 9, 27, 18, 50,0);
                    }
                )).Returns(true);

            mock.Setup(v => v.GetLocalTime(
                this._paris,
                this._refDate,
                ref It.Ref<DateTime>.IsAny))
                .Callback(new LocalTimeCallBack(
                    (Location val, DateTime dd, ref DateTime rr) =>
                    {
                        rr = new DateTime(2018, 9, 27, 15, 20, 0);
                    }
                )).Returns(true);

            TimeZoneHelper.Api = mock.Object;

            var utcMessage = TimeZoneHelper.GetDiff(this._paris, this._calcutta, this._refDate);

            if (!utcMessage.IsReceived)
            {
                Assert.Fail();
            }
            else
            {
                Assert.AreEqual(new TimeSpan(-3, -30, 0), utcMessage.Offset);
            }
        }

        [Test]
        public void Should_Be_Failed_When_First_Local_Time_Is_Wrong()
        {

            var mock = new Mock<ITimeZoneApi>();
            mock.Setup(v => v.GetLocalTime(
                    this._calcutta,
                    this._refDate,
                    ref It.Ref<DateTime>.IsAny)).Returns(false);

            mock.Setup(v => v.GetLocalTime(
                this._paris,
                this._refDate,
                ref It.Ref<DateTime>.IsAny)).Returns(true);

            TimeZoneHelper.Api = mock.Object;

            var utcMessage = TimeZoneHelper.GetDiff(this._paris, this._calcutta, this._refDate);

            Assert.AreEqual(false, utcMessage.IsReceived);
        }

        [Test]
        public void Should_Be_Failed_When_Second_Local_Time_Is_Wrong()
        {
            var mock = new Mock<ITimeZoneApi>();
            mock.Setup(v => v.GetLocalTime(
                    this._calcutta,
                    this._refDate,
                    ref It.Ref<DateTime>.IsAny)).Returns(true);

            mock.Setup(v => v.GetLocalTime(
                this._paris,
                this._refDate,
                ref It.Ref<DateTime>.IsAny)).Returns(false);

            TimeZoneHelper.Api = mock.Object;

            var utcMessage = TimeZoneHelper.GetDiff(this._paris, this._calcutta, this._refDate);

            Assert.AreEqual(false, utcMessage.IsReceived);
        }

        [Test]
        public void Should_Be_3Hours_and_30minutes_between_Paris_and_Calcutta_When_Using_Google_Api()
        {
            TimeZoneHelper.Api = new GoogleTimeZoneApi();

            var date = new DateTime(2018, 09, 27, 13, 20, 0);
            var calcutta = new Location()
            {
                Latitude = 22.6520429,
                Longitude = 88.4463299
            };

            var parisLocation = new Location()
            {
                Latitude = 49.0096906,
                Longitude = 2.547924500000022
            };

            var utcMessage = TimeZoneHelper.GetDiff(parisLocation, calcutta, date);

            if (!utcMessage.IsReceived)
            {
                Assert.Fail();
            }
            else
            {
                Assert.AreEqual(new TimeSpan(-3, -30, 0), utcMessage.Offset);
            }
        }
    }
}
