using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IPetsRepository
{
    public List<Pets> GetAllPets();
    public Pets AddPets(Pets pets); // Pets pets som parameter ?
    public Pets GetPetsById(int id);
    public void RebuildDB();
    public Pets UpdatePets(Pets pets);
    public Pets DeletePets(Pets pets);
}