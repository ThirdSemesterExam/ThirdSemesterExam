using Application.Interfaces;
using Domain;
using Moq;
using Application;
namespace XUnitTest
{
    public class PetsServiceTest
    {
        private List<Pets> fakeRepo = new List<Pets>();
        private Mock<IPetsRepository> petsRepoMock = new Mock<IPetsRepository>();

        public PetsServiceTest()
        {
            petsRepoMock.Setup(x => x.GetAllPets()).Returns(fakeRepo);
            petsRepoMock.Setup(x => x.GetPetsById(It.IsAny<int>())).Returns<int>(id => fakeRepo.FirstOrDefault(x => x.Id == id));
            petsRepoMock.Setup(x => x.AddPets(It.IsAny<Pets>())).Callback<Pets>(p => fakeRepo.Add(p));
            petsRepoMock.Setup(x => x.UpdatePets(It.IsAny<Pets>())).Callback<Pets>(p =>
            {
                var index = fakeRepo.IndexOf(p);
                if (index != -1)
                    fakeRepo[index] = p;
            });
            petsRepoMock.Setup(x => x.DeletePets(It.IsAny<Pets>())).Callback<Pets>(p => fakeRepo.Remove(p));
        }

        #region Create PetsService
        [Fact]
        public void CreatePetsService_ValidPetsService_Test()
        {
            // Arrange
            Mock<IPetsRepository> repoMock = new Mock<IPetsRepository>();

            PetsService? service = null;

            // Act
            service = new PetsService(repoMock.Object);

            // Assert
            Assert.NotNull(service);
            Assert.True(service is PetsService);
        }

        [Fact]
        public void CreatePetsService_NullRepository_ExpectArgumentExceptionTest()
        {
            // Arrange
            PetsService? service;

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() => service = new PetsService(null));
            Assert.Equal("Missing PetsRepository", ex.Message);
        }
        #endregion


        #region AddPets
        [Theory]
        [InlineData(1, "Name", "Address", 1234, "City", "Email")]
        [InlineData(2, "Name", "Address", 1234, "City", null)]
        public void AddPets_ValidPets_Test(int id, string name, string address, int zipcode, string city, string email)
        {
            // Arrange
            Pets pets = new Pets(id, name, address, zipcode, city, email);

            var service = new PetsService(petsRepoMock.Object);

            // Act
            service.AddPets(pets);

            // Assert
            //Assert.True(fakeRepo.Count == 1);
            //Assert.Equal(s, fakeRepo[0]);
            petsRepoMock.Verify(r => r.AddPets(pets), Times.Once);
        }

        [Fact]
        public void AddPets_PetsIsNull_ExpectArgumentException_Test()
        {
            // Arrange
            var service = new PetsService(petsRepoMock.Object);

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() => service.AddPets(null)); // Kig i interface??
            Assert.Equal("Pets is missing", ex.Message);
            petsRepoMock.Verify(r => r.AddPets(null), Times.Never);
        }

        [Theory]
        [InlineData(0, "Name", "Address", 1234, "City", "Email", "Invalid id")]
        [InlineData(1, null, "Address", 1234, "City", "Email", "Invalid name")]
        [InlineData(1, "", "Address", 1234, "City", "Email", "Invalid name")]
        [InlineData(1, "Name", null, 1234, "City", "Email", "Invalid address")]
        [InlineData(1, "Name", "", 1234, "City", "Email", "Invalid address")]
        [InlineData(1, "Name", "Address", 0, "City", "Email", "Invalid zipcode")]
        [InlineData(1, "Name", "Address", 10000, "City", "Email", "Invalid zipcode")]
        [InlineData(1, "Name", "Address", 1234, null, "Email", "Invalid city")]
        [InlineData(1, "Name", "Address", 1234, "", "Email", "Invalid city")]
        [InlineData(1, "Name", "Address", 1234, "City", "", "Invalid email")]
        public void AddStudent_InvalidStudent_ExpectArgumentException(int id, string name, string address, int zipcode, string city, string email, string expectedMessage)
        {
            var service = new PetsService(petsRepoMock.Object);

            var pets = new Pets(id, name, address, zipcode, city, email);

            var ex = Assert.Throws<ArgumentException>(() => service.AddPets(pets));
            Assert.Equal(expectedMessage, ex.Message);
            petsRepoMock.Verify(r => r.AddPets(pets), Times.Never);
        }

        [Fact]
        public void AddPets_DuplicatedId_ExpectArgumentException_Test()
        {
            // Arrange
            var existingPets = new Pets(1, "name", "address", 1234, "city", "email");
            petsRepoMock.Setup(r => r.GetPetsById(1)).Returns(() => existingPets);


            var service = new PetsService(petsRepoMock.Object);

            // Act + assert
            var ex = Assert.Throws<ArgumentException>(() => service.AddPets(existingPets));
            Assert.Equal("Pets already exist", ex.Message);
            petsRepoMock.Verify(r => r.AddPets(existingPets), Times.Never);
        }

        #endregion // AddPets
    }
}
