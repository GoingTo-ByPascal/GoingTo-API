﻿using GoingTo.API.Resources.Accounts;
using GoingTo_API.Domain.Models;
using GoingTo_API.Domain.Models.Geographic;
using GoingTo_API.Resources;

namespace GoingTo_API.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveProfileResource, Domain.Models.Profile>();
            CreateMap<SaveWalletResource, Wallet>();

            CreateMap<SaveAchievementResource, Achievement>();
            CreateMap<SavePlaceResource, Place>();
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<SaveReviewResource, Review>();
            CreateMap<SaveLanguageResource, Language>();
            CreateMap<SaveCurrencyResource, Currency>();
            CreateMap<SaveTipResource, Tip>();
        }
    }
}

