using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{

    private static readonly Dictionary<Guid, Breakfast> _breakfasts= new();
//each id maps to a breakfast//static so it will be shared across all instances of this class
    public void CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);//key:id, val:breakfast//instead u should add it to db
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        //return _breakfasts[id];
        // if (_breakfasts.ContainsKey(id))
        // {
        //     return _breakfasts[id];
        // }
        if(_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        return Errors.Breakfast.NotFound;
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
    }

    public void UpsertBreakfast(Breakfast breakfast)
    {
        _breakfasts[breakfast.Id] = breakfast;
    }
}