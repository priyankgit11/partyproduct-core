﻿using AutoMapper;
using PartyProductCore.DTO;
using PartyProductCore.Models;

namespace PartyProductCore.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TblParty, PartyDTO>().ReverseMap();
            CreateMap<PartyCreationDTO, TblParty>();
            CreateMap<TblProduct, ProductDTO>().ReverseMap();
            CreateMap<ProductCreationDTO, TblProduct>();
            CreateMap<TblProductRate, ProductRateRelationDTO>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).ReverseMap();
            CreateMap<ProductRateCreationDTO, TblProductRate>();
            CreateMap<TblAssignParty, AssignPartyDTO>().ReverseMap();
            CreateMap<AssignPartyCreationDTO, TblAssignParty>();
            CreateMap<TblAssignParty, AssignPartyRelationDTO>().ForMember(dest => dest.PartyName, opt => opt.MapFrom(src => src.Party.PartyName)).ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).ReverseMap();
            CreateMap<TblInvoiceDetail, InvoiceDetailsRelationDTO>().ForMember(dest => dest.PartyName, opt => opt.MapFrom(src => src.Party.PartyName)).ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).ReverseMap();
            CreateMap<InvoiceDetailCreationDTO,TblInvoiceDetail>();
        }
    }
}
