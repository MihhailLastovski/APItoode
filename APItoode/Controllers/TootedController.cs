using APItoode.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using ToodeAPI.Models;

namespace veebirakendus.Controllers;
[Route("[controller]")]
[ApiController]
public class TootedController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TootedController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public List<Toode> Get()
    {
        return _context.Tooted.ToList(); ;
    }

    [HttpDelete("kustuta/{id}")]
    public List<Toode> Delete(int id)
    {
        var toode = _context.Tooted.Find(id);

        if (toode == null)
        {
            return _context.Tooted.ToList();
        }

        _context.Tooted.Remove(toode);
        _context.SaveChanges();
        return _context.Tooted.ToList();
    }

    [HttpPost("lisa/{nimi}/{hind}/{aktiivne}/{kogus}")]
    public List<Toode> Add(string nimi, double hind, bool aktiivne, int kogus)
    {
        bool check = true;
        foreach (var item in _context.Tooted)
        {
            if (item.Name == nimi && item.Price == hind)
            {
                check = false;
                item.Kogus += kogus;
                if (item.IsActive == false)
                {
                    item.IsActive = true;

                }
                _context.Tooted.Update(item);

            }
        }
        _context.SaveChanges();

        if (check)
        {
            _context.Tooted.Add(new Toode(nimi, hind, aktiivne, kogus));
            _context.SaveChanges();
        }

        return _context.Tooted.ToList();
    }


    [HttpGet("hind-dollaritesse/{kurss}")]
    public List<Toode> Dollaritesse(double kurss)
    {
        if (kurss == 1)
            return _context.Tooted.ToList();

        List<Toode> listT = new List<Toode>();
        foreach (var t in _context.Tooted)
        {
            t.Price *= kurss;
            listT.Add(t);
        }

        return listT;
    }
}