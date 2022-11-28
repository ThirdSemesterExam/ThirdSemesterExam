using Application.Interfaces;
using Domain;
using Moq;
using Application;
using Application.Validators;
using Application.DTOs;
using AutoMapper;
using System.Diagnostics;
using System.Net;
using System.Reflection.Emit;
using System.Xml.Linq;

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
        [InlineData(1, "Name", "Address", 1234, "City", "Email", "DogBreeds", 123, "Description")]
        [InlineData(2, "Name", "Address", 1234, "City", null, "DogBreeds", 123, "Description")]
        public void AddPets_ValidPets_Test(int id, string name, string address, int zipcode, string city, string email, string dogBreeds, int price, string description)
        {
            // Arrange
            PostPetsDTO postPets = new PostPetsDTO(name, address, zipcode, city, email, dogBreeds, price, description);
            Pets pets = new Pets(id, name, address, zipcode, city, email, dogBreeds, price, description);

            var mapper = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<PostPetsDTO, Pets>();
            }).CreateMapper();
            var service = new PetsService(petsRepoMock.Object, new PostPetsValidator(), new PetsValidator(), mapper);

            // Act
            service.AddPets(postPets);

            // Assert
            Assert.True(fakeRepo.Count == 1);
            Assert.Equal(pets.Name, fakeRepo[0].Name);
            Assert.Equal(pets.Price, fakeRepo[0].Price);
            Assert.Equal(pets.Address, fakeRepo[0].Address);
            Assert.Equal(pets.Zipcode, fakeRepo[0].Zipcode);
            Assert.Equal(pets.Email, fakeRepo[0].Email);
            Assert.Equal(pets.DogBreeds, fakeRepo[0].DogBreeds);
            //petsRepoMock.Verify(r => r.AddPets(pets), Times.Once);
        }
        /*
        [Fact]
        public void AddPets_PetsIsNull_ExpectArgumentException_Test()
        {
            // Arrange
            var mapper = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Application.DTOs.PostPetsDTO, Pets>();
            }).CreateMapper();
            var service = new PetsService(petsRepoMock.Object, new PostPetsValidator(), new PetsValidator(), mapper);

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() => service.AddPets(null)); // Kig i interface??
            Assert.Equal("Pets is missing", ex.Message);
            petsRepoMock.Verify(r => r.AddPets(null), Times.Never);
        }
        */
        /*
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
        public void AddPets_InvalidPets_ExpectArgumentException(int id, string name, string address, int zipcode, string city, string email, string expectedMessage)
        {
            var service = new PetsService(petsRepoMock.Object);

            var pets = new Pets(id, name, address, zipcode, city, email);

            var ex = Assert.Throws<ArgumentException>(() => service.AddPets(pets));
            Assert.Equal(expectedMessage, ex.Message);
            petsRepoMock.Verify(r => r.AddPets(pets), Times.Never);
        }
        */
        [Fact]
        public void AddPets_DuplicatedId_ExpectArgumentException_Test()
        {
            // Arrange
            PostPetsDTO postPets = new PostPetsDTO(1, "name", "address", 1234, "city", "email", "dogbreed", 123, "description");

            var existingPets = new Pets(1, "name", "address", 1234, "city", "email", "dogbreed", 123, "description");
            petsRepoMock.Setup(r => r.GetPetsById(1)).Returns(() => existingPets);
            var mapper = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<PostPetsDTO, Pets>();
            }).CreateMapper();
            var service = new PetsService(petsRepoMock.Object, new PostPetsValidator(), new PetsValidator(), mapper);

            // Act + assert
            var ex = Assert.Throws<ArgumentException>(() => service.AddPets(postPets));
            Assert.Equal("Pets already exist", ex.Message);
            petsRepoMock.Verify(r => r.AddPets(existingPets), Times.Never);
        }
        

        #endregion // AddPets

    }
}
