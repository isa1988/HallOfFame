using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using HallOfFame.Core.Contracts;
using HallOfFame.Core.Contracts.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Service;
using HallOfFame.Service.Contracts.AreaPerson;
using HallOfFame.Service.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;
using HallOfFame.Service.Services.AreaPerson;
using Moq;
using Xunit;

namespace HallOfFame.ServiceUnitTests.AreaPerson
{
    public class PersonSkillServiceTest
    {
        /// <summary>
        /// Тест на состояние
        /// </summary>
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public async void Check_GetByIdAsync(string partnerIdStr)
        {
            // Arrange
            var partnerId = long.Parse(partnerIdStr);
            var partner = new Person
            {
                Id = 2,
                FirstName = "Имя",
                SurName = "Фамилия",
            };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<PersonDto>(It.IsAny<Person>()))
                .Returns((Person source) =>
                {
                    if (source == null) 
                        return null;
                    return new PersonDto
                    {
                        FirstName = source.FirstName,
                        SurName = source.SurName,
                        Id = source.Id,
                    };
                });
            var mockSkillOfLevelService = new Mock<ISkillOfLevelService>();
            var mock = new Mock<IPersonRepository>();
            mock.Setup(repo => repo.GetByIdAsync(2, null)).ReturnsAsync(partner);
            var service = new PersonSkillService(mockMapper.Object, mock.Object, mockSkillOfLevelService.Object);

            // Act
            var result = await service.GetByIdAsync(partnerId);

            // Assert
            Assert.IsType<EntityOperationResult<PersonDto>>(result);
            if (partnerId == 2)
                Assert.Equal(2, result.Entity.Id);
            else
            {
                Assert.Null(result.Entity);
            }
        }

        /// <summary>
        /// проверка на всех
        /// </summary>
        [Fact]
        public async void Check_GetAllAsync()
        {
            // Arrange

            var persons = new List<Person>();
            persons.Add(new Person
            {
                Id = 1,
                FirstName = "Имя",
                SurName = "Фамилия",
            });
            persons.Add(new Person
            {
                Id = 2,
                FirstName = "Имя2",
                SurName = "Фамилия2",
            });
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<PersonDto>>(It.IsAny<List<Person>>()))
                .Returns((List<Person> source) =>
                {
                    if (source == null || source.Count == 0)
                        return new List<PersonDto>();
                    var persons = new List<PersonDto>();
                    for (int i = 0; i < source.Count; i++)
                    {
                        persons.Add(new PersonDto
                        {
                            FirstName = source[i].FirstName,
                            SurName = source[i].SurName,
                            Id = source[i].Id,
                        });
                    }

                    return persons;
                });
            var mockSkillOfLevelService = new Mock<ISkillOfLevelService>();
            var mock = new Mock<IPersonRepository>();
            mock.Setup(repo => repo.GetAllAsync(null)).ReturnsAsync(persons);
            var service = new PersonSkillService(mockMapper.Object, mock.Object, mockSkillOfLevelService.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.IsType<List<PersonDto>>(result);
            Assert.Equal(2, result.Count);
        }
    }
}
