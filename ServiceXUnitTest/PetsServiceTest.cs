using Application.Interfaces;
using Domain;
using Moq;
using Application;
using Application.DTOs;
using Application.Validators;
using AutoMapper;

namespace XUnitTest;


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
        */
    }
    
 