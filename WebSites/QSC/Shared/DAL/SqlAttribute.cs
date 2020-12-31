using System;
using System.Data;

using Debug = System.Diagnostics.Debug;

namespace DAL
{
[ AttributeUsage(AttributeTargets.Method) ]
public sealed class SqlCommandMethodAttribute : Attribute
{
private string _commandText;
private CommandType _commandType;

public SqlCommandMethodAttribute(CommandType commandType) : 
this(CommandType.StoredProcedure, null) {}
        
public SqlCommandMethodAttribute(CommandType commandType, string commandText) 
{
_commandText = commandText;
_commandType = commandType;
}

public string CommandText
{
get { return _commandText == null ? string.Empty : _commandText; }
set { _commandText = value; }
}

public CommandType CommandType
{
get { return _commandType; }
set { _commandType = value; }
}
}
    
[ AttributeUsage(AttributeTargets.Parameter) ]
public class SqlParameterAttribute : Attribute
{
private string _name;
private bool _paramTypeDefined;
private SqlDbType _paramType;
private int _size;
private byte _precision;
private byte _scale;
private bool _directionDefined;
private ParameterDirection _direction;

public SqlParameterAttribute() {_direction=ParameterDirection.Input;}

public SqlParameterAttribute(string name)
{
Name = name;
}

public SqlParameterAttribute(int size, ParameterDirection dir)
{
Size = size;
_direction=dir;
}

public SqlParameterAttribute(SqlDbType paramType, ParameterDirection dir)
{
SqlDbType = paramType;
	_direction=dir;
	_directionDefined=true;
}

public SqlParameterAttribute(string name, SqlDbType paramType)
{
Name = name;
SqlDbType = paramType;
}

public SqlParameterAttribute(SqlDbType paramType, int size, ParameterDirection dir)
{
SqlDbType = paramType;
Size = size;
_direction=dir;
	_directionDefined=true;
}

public SqlParameterAttribute(string name, int size)
{
Name = name;
Size = size;
}

public SqlParameterAttribute(string name, SqlDbType paramType, int size)
{
Name = name;
SqlDbType = paramType;
Size = size;
}

public string Name
{
get { return _name == null ? string.Empty : _name; }
set { _name = value; }
}

public int Size
{
get { return _size; }
set { _size = value; }
}

public byte Precision
{
get { return _precision; }
set { _precision = value; }
}

public byte Scale
{
get { return _scale; }
set { _scale = value; }
}

public ParameterDirection Direction
{
get 
{ 
Debug.Assert(_directionDefined);
return _direction; 
}

set 
{ 
_direction = value; 
_directionDefined = true;
}
}
        
public SqlDbType SqlDbType
{
get 
{ 
Debug.Assert(_paramTypeDefined);
return _paramType; 
}

set 
{ 
_paramType = value; 
_paramTypeDefined = true;
}
}

public bool IsNameDefined
{
get { return _name != null && _name.Length != 0; }
}

public bool IsSizeDefined
{
get { return _size != 0; }
}

public bool IsTypeDefined
{
get { return _paramTypeDefined; }
}

public bool IsDirectionDefined
{
get { return _directionDefined; }
}

public bool IsScaleDefined
{
get { return _scale != 0; }
}

public bool IsPrecisionDefined
{
get { return _precision != 0; }
}
}
}

namespace DAL
{
[ AttributeUsage(AttributeTargets.Parameter) ]
public sealed class NonCommandParameterAttribute : Attribute
{
}
}
