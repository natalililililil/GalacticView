using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasData
            (
            new News
            {
                Id = Guid.NewGuid(),
                URL = "/news",
                Title = "Новость1",
                Subtitle = "это подзаголовок",
                Text = "Software developer ваыраыоаыи ыраггыак ико кррк уоарк к а",
                TitleImagePath = "путь1",
            }
            );
        }

    }
}
