using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CenterForGeneticResearch.Models.Enums;

namespace CenterForGeneticResearch.Helpers;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? enumValue.ToString();
    }
    
    public static string GetEmployeePhoto(this EmployeeType employeeType)
    {
        return $"/images/employees/{employeeType.ToString().ToLower()}.png";
    }
}