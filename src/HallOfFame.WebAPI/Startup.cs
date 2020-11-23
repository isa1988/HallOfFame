using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using HallOfFame.Core.Contracts.AreaPerson;
using HallOfFame.Core.Contracts.AreaPerson.AreaSkill;
using HallOfFame.DAL.Data;
using HallOfFame.DAL.Repository.AreaPerson;
using HallOfFame.DAL.Repository.AreaPerson.AreaSkill;
using HallOfFame.Service.Contracts.AreaPerson;
using HallOfFame.Service.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Service.Services.AreaPerson;
using HallOfFame.Service.Services.AreaPerson.AreaSkill;
using HallOfFame.WebAPI.AppStart;
using HallOfFame.WebAPI.AppStart.AutoMapper;

namespace HallOfFame.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseContext(Configuration);
            services.AddAutoMapperCustom();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDataBaseInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            dbInitializer.Initialize();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<SkillRepository>().As<ISkillRepository>();
            builder.RegisterType<SkillOfLevelRepository>().As<ISkillOfLevelRepository>();
            builder.RegisterType<SkillOfPersonRepository>().As<ISkillOfPersonRepository>();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();

            builder.RegisterType<DataBaseInitializer>().As<IDataBaseInitializer>();

            builder.RegisterType<SkillOfLevelService>().As<ISkillOfLevelService>();
            builder.RegisterType<PersonService>().As<IPersonService>();
            builder.RegisterType<PersonSkillService>().As<IPersonSkillService>();
        }
    }
}
