using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Personal;

namespace step_buy_server.controller.logistics;

[Route("api/Address/")]
[ApiController]
public class AddressController: ControllerBase
{
    private readonly AppDBConfig _context;

    public AddressController(AppDBConfig context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<Address>>> getAddresses(string userId)
    {
        var addresses =  await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        return addresses;
    }

    [HttpPost]
    public async Task<ActionResult<Address>> Post(AddressDTO _address)
    {
        Address address = new Address()
        {
            HouseNo = _address.HouseNo,
            PlotNo = _address.PlotNo,
            Pin =  _address.Pin,
            Country = _address.Country,
            AreaName = _address.AreaName,
            ColonyName = _address.ColonyName,
            CityName = _address.CityName,
            BuildingName = _address.BuildingName,
            NameOfReciver = _address.NameOfReciver,
            AlternatePhone = _address.AlternatePhone,
            UserId = _address.UserId,
            RoadNumber = _address.RoadNumber,
            State = _address.State,
            DistrictName = _address.DistrictName,
        };

        try
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
        return Ok(address);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Address>> Delete(string id)
    {
        var addr = await _context.Addresses.FirstOrDefaultAsync(ad => ad.Id == id);
        if (addr == null)
            return NotFound();
        try
        {
            _context.Addresses.Remove(addr);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Address>> Put(AddressDTO _address, string id)
    {
        var addr = await _context.Addresses.FirstOrDefaultAsync(ad => ad.Id == id);
        if (addr == null)
            return NotFound();
        if (!addr.UserId.Equals(addr.UserId))
        {
            return NoContent();
        }
        
        addr.HouseNo = _address.HouseNo;
        addr.BuildingName = _address.BuildingName;
        addr.PlotNo = _address.PlotNo;
        addr.RoadNumber = _address.RoadNumber;
        addr.ColonyName = _address.ColonyName;
        addr.AreaName = _address.AreaName;
        addr.CityName = _address.CityName;
        addr.DistrictName = _address.DistrictName;
        addr.State = _address.State;
        addr.Country = _address.Country;
        addr.Pin = _address.Pin;
        addr.AlternatePhone = _address.AlternatePhone;
        addr.NameOfReciver = _address.NameOfReciver;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }

        return Ok(addr);
    }
}