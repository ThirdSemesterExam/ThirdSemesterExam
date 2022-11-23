﻿using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IPetsService
{
    public List<Pets> GetAllPets();
    public Pets CreateNewPets(PostPetsDTO dto);
    public Pets GetPetsById(int id);
    public void RebuildDB();
    public Pets UpdatePets(int id, Pets product);
    public Pets DeletePets(int id);

}