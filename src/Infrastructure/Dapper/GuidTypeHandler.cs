using System.Data;
using Dapper;

namespace Infrastructure.Dapper;

public class GuidTypeHandler : SqlMapper.ITypeHandler
{
    public void SetValue(IDbDataParameter parameter, object value)
    {
        parameter.Value = value.ToString();
    }

    public object Parse(Type destinationType, object value)
    {
        return value is Guid  ? value : Guid.Parse(value.ToString()!);
    }
}