using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TvShowProject;

namespace UnitTestGenre
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //successful test
        public void TestGenre()
        {
            //Arrange
            Show testShow = new Show();
            testShow.Genre = "Comedy";
            string genre = "Comedy";

            //Act
            testShow.GenreCheck(genre);

            //Assert
            Assert.AreEqual(genre, testShow.Genre);
        }

        [TestMethod]
        //Test fail
        public void TestGenreFail()
        {
            //Arrange
            Show testShow = new Show();
            testShow.Genre = "Horror";
            string genre = "Apple";

            //Act
            testShow.GenreCheck(genre);

            //Assert
            Assert.AreEqual(genre, testShow.Genre);
        }
    }
}
