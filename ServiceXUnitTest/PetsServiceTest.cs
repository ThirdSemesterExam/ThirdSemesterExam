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
using System.Collections.Generic;
using System.Collections;

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
            //petsRepoMock.Setup(x => x.DeletePets(It.IsAny<Pets>())).Callback<Pets>(p => fakeRepo.Remove(p));
            /*
            private Dictionary<string, string> WordCache { get; set; } =
        new Dictionary<string, string>();


             var fakeRepo = new Dictionary<int,Pets>();
            */
        }


    public class PetsServiceTest
    {
        [Fact]
        public void CreatePetsService()
        {
            // Arrange 
            Mock<IPetsRepository> mockRepository = new Mock<IPetsRepository>();
            var mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<PostPetsDTO, Pets>();
            }).CreateMapper();
            var postDeviceValidator = new PostPetsValidator();
            var putDeviceValidator = new PutPetsValidator();

            // Act 
            IPetsService service = new PetsService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

            // Assert
            Assert.NotNull(service);
            Assert.True(service is PetsService);
        }
        
        
     
        [Theory]
        [InlineData(1, "", "dog", "Dog name cannot be empty or null")]                  //Invalid pets with empty petsName
         public void CreateInvalidDeviceTest(int petsId, string petsName, string description, string expectedMessage)
        {
            // Arrange
            Mock<IPetsRepository> mockRepository = new Mock<IPetsRepository>();
            var mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<PostPetsDTO, Pets>();
            }).CreateMapper();
            var postDeviceValidator = new PostPetsValidator();
            var putDeviceValidator = new PutPetsValidator();
            IPetsService service = new PetsService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

            Pets pets = new Pets{Id = petsId, Name = petsName, Description = description};
            PostPetsDTO dto = new PostPetsDTO { Name = petsName, DogBreeds = description };
            // Act 
            //var action = () => service.AddDevice(dto);
            var action = () => service.CreateNewPets(dto);

            // Assert
            var ex = Assert.Throws<ArgumentException>(action);
        
            Assert.Equal(expectedMessage, ex.Message);
            mockRepository.Verify(r => r.AddPets(pets), Times.Never);
        }
       
        
         /*
        [Theory]
        [InlineData(1, "dog", "123")]
        public void CreateValidPetsTest(int petsId, string petsName, string description)
        {
            // Arrange
            List<Pets> dogs = new List<Pets>();
            Pets pet = new Pets{Id = petsId, Name = petsName, Description = description};
            PostPetsDTO dto = new PostPetsDTO { Name = petsName, Description = description};
        
            Mock<IPetsRepository> mockRepository = new Mock<IPetsRepository>();
            var mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<PostPetsDTO, Pets>();
            }).CreateMapper();
            
            var postDeviceValidator = new PostPetsValidator();
            var putDeviceValidator = new PutPetsValidator();
        
            IPetsService service = new PetsService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
            mockRepository.Setup(r => r.AddPets(It.IsAny<Pets>())).Returns(() =>
            {
                dogs.Add(pet);
                return pet;
            });
        
            // Act 
            var createdPet = service.CreateNewPets(dto);
        
            // Assert
            Assert.True(dogs.Count == 1);
            Assert.Equal(pet.Id, createdPet.Id);
            Assert.Equal(pet.Name, createdPet.Name);
            Assert.Equal(pet.Description, createdPet.Description);
            mockRepository.Verify(r => r.AddPets(It.IsAny<Pets>()), Times.Once);
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

        [Fact]
        public void AddPets_PetsIsNull_ExpectArgumentException_Test()
        {
            // Arrange
            var mapper = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<PostPetsDTO, Pets>();
            }).CreateMapper();
            var service = new PetsService(petsRepoMock.Object, new PostPetsValidator(), new PetsValidator(), mapper);

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() => service.AddPets(null)); // Kig i interface??
            Assert.Equal("Pets is missing", ex.Message);
            petsRepoMock.Verify(r => r.AddPets(null), Times.Never);
        }
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
        
        
        
        #region RemovePets

        [Fact]
        public void RemovePets_ValidPets_Test()
        {
            // Arrange
            var p1 = new Pets(1, "name1", "address1", 1234, "city1", "email1", "dogbreed1", 123, "description1");
            var p2 = new Pets(2, "name2", "address2", 2345, "city2", "email2", "dogbreed2", 234, "description2");

            var fakeRepo = new Dictionary<int,Pets>();
            fakeRepo.Add(p1.Id,p1);
            fakeRepo.Add(p2.Id,p2);

            Mock<IPetsRepository> repoMock = new Mock<IPetsRepository>();
            repoMock.Setup(r => r.DeletePets(It.IsAny<int>())).Callback<int>(id => fakeRepo.Remove(id));
            repoMock.Setup(r => r.GetPetsById(It.IsAny<int>())).Returns<int>(id => fakeRepo[id]);


            var service = new PetsService(repoMock.Object);

            // Act
            service.DeletePets(p1.Id);

            // Assert
            Assert.True(fakeRepo.Count == 1);
            Assert.Contains(p2, fakeRepo.Values);
            Assert.DoesNotContain(p1, fakeRepo.Values);
            repoMock.Verify(r => r.DeletePets(p1.Id), Times.Once);
        }
        /*
        [Fact]
        public void RemovePets_PetsIsNull_ExpectArgumentException()
        {
            // Arrange
            Mock<IPetsRepository> repoMock = new Mock<IPetsRepository>();
            var service = new PetsService(repoMock.Object);

            // Act and assert
            var ex = Assert.Throws<ArgumentException>(() => service.DeletePets(null));
            Assert.Equal("Pets is missing", ex.Message);
            repoMock.Verify(r => r.DeletePets(null), Times.Never);
        }
        */
        
        [Fact]
        public void RemovePets_PetsDoesNotExist_ExpectArgumentException()
        {
            // Arrange
            var p1 = new Pets(1, "name1", "address1", 1234, "city1", "email1", "dogbreed1", 123, "description1");
            var p2 = new Pets(2, "name2", "address2", 2345, "city2", "email2", "dogbreed2", 234, "description2");

            var fakeRepo = new Dictionary<int, Pets>();
            fakeRepo.Add(p1.Id, p1);
            fakeRepo.Add(p2.Id, p2);

            Mock<IPetsRepository> repoMock = new Mock<IPetsRepository>();
            repoMock.Setup(r => r.DeletePets(It.IsAny<int>())).Callback<int>(p =>
            {
                fakeRepo.Remove(p);
            });

            var service = new PetsService(repoMock.Object);

            // Act + assert
            var ex = Assert.Throws<ArgumentException>(() => service.DeletePets(p2.Id));
            Assert.Equal("Pets does not exist", ex.Message);
            Assert.Contains(p1, fakeRepo.Values);
            repoMock.Verify(r => r.DeletePets(p2.Id), Times.Never);
        }

        #endregion // RemovePets

    }
}
