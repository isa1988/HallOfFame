﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Core.Contracts;
using HallOfFame.Core.Contracts.AreaPerson;
using HallOfFame.Core.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Core.Entity;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.DAL.Data
{
    public class DataBaseInitializer : IDataBaseInitializer
    {
        public DataBaseInitializer(IServiceProvider serviceProvider, 
                                   ISkillRepository skillRepository,
                                   ISkillOfLevelRepository skillOfLevelRepository,
                                   ISkillOfPersonRepository skillOfPersonRepository, 
                                   IPersonRepository personRepository)
        {
            this.serviceProvider = serviceProvider;
            this.skillRepository = skillRepository;
            this.skillOfLevelRepository = skillOfLevelRepository;
            this.skillOfPersonRepository = skillOfPersonRepository;
            this.personRepository = personRepository;
        }

        private readonly IServiceProvider serviceProvider;
        private readonly ISkillRepository skillRepository;
        private readonly ISkillOfLevelRepository skillOfLevelRepository;
        private readonly ISkillOfPersonRepository skillOfPersonRepository;
        private readonly IPersonRepository personRepository;

        public async Task InitializeAsync()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HallOfFameContex>();
                context.Database.EnsureCreated();

                if (skillRepository.IsEqualsAsyncTask("SQL").Result)
                    return;

                var skills = await SkillAddDBAsync();
                var skillOfLevels = await SkillOfLevelAddDBAsync(skills);
                var persons = await PersonAddDBAsync();
                await SkillOfPersonAddDBAsync(persons, skillOfLevels);
            }
        }

        private async Task<List<Skill>> SkillAddDBAsync()
        {
            var skills = new List<Skill>();
            skills.Add(new Skill { Name = "SQL" });
            skills.Add(new Skill { Name = "English" });
            skills.Add(new Skill { Name = "Stress" });
            skills.Add(new Skill { Name = "CSharp" });
            skills.Add(new Skill { Name = "VS2019" });
            skills.Add(new Skill { Name = "Russian" });
            
            await SaveOperationAsync(skills, skillRepository);
            
            return skills;
        }

        private async Task<List<SkillOfLevel>> SkillOfLevelAddDBAsync(List<Skill> skills)
        {
            var skillOfLevels = new List<SkillOfLevel>();
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[0].Id, Level = 10 });//0
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[0].Id, Level = 5 });//1
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[1].Id, Level = 10 });//2
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[1].Id, Level = 5 });//3
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[2].Id, Level = 10 });//4
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[2].Id, Level = 5 });//5
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[3].Id, Level = 10 });//6
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[3].Id, Level = 5 });//7
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[4].Id, Level = 10 });//8
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[4].Id, Level = 5 });//9
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[5].Id, Level = 10 });//10
            skillOfLevels.Add(new SkillOfLevel { SkillId = skills[5].Id, Level = 5 });//11
            
            await SaveOperationAsync(skillOfLevels, skillOfLevelRepository);
            
            return skillOfLevels;
        }

        private async Task<List<Person>> PersonAddDBAsync()
        {
            var persons = new List<Person>();
            persons.Add(new Person { SurName = "Пктров", FirstName = "Анатолий" });
            persons.Add(new Person { SurName = "Пупкин", FirstName = "Василмй" });
            persons.Add(new Person { SurName = "Соколов", FirstName = "Никита" });

            await SaveOperationAsync(persons, personRepository);

            return persons;
        }

        private async Task<List<SkillOfPerson>> SkillOfPersonAddDBAsync(List<Person> persons, List<SkillOfLevel> skillOfLevels)
        {
            var skillOfPersons = new List<SkillOfPerson>();
            skillOfPersons.Add(new SkillOfPerson { PersonId = persons[0].Id, SkillOfLevelId = skillOfLevels[0].Id });
            skillOfPersons.Add(new SkillOfPerson { PersonId = persons[0].Id, SkillOfLevelId = skillOfLevels[5].Id });
            skillOfPersons.Add(new SkillOfPerson { PersonId = persons[1].Id, SkillOfLevelId = skillOfLevels[4].Id });
            skillOfPersons.Add(new SkillOfPerson { PersonId = persons[1].Id, SkillOfLevelId = skillOfLevels[7].Id });
            skillOfPersons.Add(new SkillOfPerson { PersonId = persons[0].Id, SkillOfLevelId = skillOfLevels[8].Id });
            skillOfPersons.Add(new SkillOfPerson { PersonId = persons[0].Id, SkillOfLevelId = skillOfLevels[3].Id });

            await SaveOperationAsync(skillOfPersons, skillOfPersonRepository);

            return skillOfPersons;
        }

        private async Task SaveOperationAsync<TEntity>(List<TEntity> entities, IRepository<TEntity> repository)
            where TEntity : class, IEntity
        {
            for (int i = 0; i < entities.Count; i++)
            {
                await repository.AddAsync(entities[i]);
            }

            await repository.SaveAsync();
        }

        private async Task SaveOperationAsync<TEntity, TId>(List<TEntity> entities, IRepository<TEntity, TId> repository)
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            for (int i = 0; i < entities.Count; i++)
            {
                await repository.AddAsync(entities[i]);
            }

            await repository.SaveAsync();
        }
    }
}
