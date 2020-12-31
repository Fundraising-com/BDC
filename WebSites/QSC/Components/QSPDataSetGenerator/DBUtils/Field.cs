using System;
using System.Data;

namespace ADONET.DbUtils
{
  /// <summary>
  /// A Helper Class to support retrieve fields 
  /// from DataReaders without regard for DbNull
  /// fields.  All accessors return default values 
  /// if the field value is DbNull.
  /// </summary>
  public class Field
  {
    // Disallow Construction
    private Field() {}

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public string GetString(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return "";
      return rec.GetString(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public decimal GetDecimal(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetDecimal(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public int GetInt(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetInt32(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public bool GetBoolean(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return false;
      return rec.GetBoolean(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public byte GetByte(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetByte(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public DateTime GetDateTime(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return new DateTime(0);
      return rec.GetDateTime(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public double GetDouble(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetDouble(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public float GetFloat(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetFloat(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Guid GetGuid(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return Guid.Empty;
      return rec.GetGuid(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Int32 GetInt32(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetInt32(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Int16 GetInt16(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetInt16(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by ordinal number.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldnum">Field Ordinal</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Int64 GetInt64(IDataRecord rec, int fldnum)
    {
      if (rec.IsDBNull(fldnum)) return 0;
      return rec.GetInt64(fldnum);
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public string GetString(IDataRecord rec, string fldname)
    {
      return GetString(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public decimal GetDecimal(IDataRecord rec, string fldname)
    {
      return GetDecimal(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public int GetInt(IDataRecord rec, string fldname)
    {
      return GetInt(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public bool GetBoolean(IDataRecord rec, string fldname)
    {
      return GetBoolean(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public byte GetByte(IDataRecord rec, string fldname)
    {
      return GetByte(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public DateTime GetDateTime(IDataRecord rec, string fldname)
    {
      return GetDateTime(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public double GetDouble(IDataRecord rec, string fldname)
    {
      return GetDouble(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public float GetFloat(IDataRecord rec, string fldname)
    {
      return GetFloat(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Guid GetGuid(IDataRecord rec, string fldname)
    {
      return GetGuid(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Int32 GetInt32(IDataRecord rec, string fldname)
    {
      return GetInt32(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Int16 GetInt16(IDataRecord rec, string fldname)
    {
      return GetInt16(rec, rec.GetOrdinal(fldname));
    }

    /// <summary>
    /// Gets a String from a DataRecord by name.
    /// </summary>
    /// <param name="rec">The DataRecord</param>
    /// <param name="fldname">Field name.</param>
    /// <returns>Contents of field.  Will return a default empty value if DbNull.</returns>
    static public Int64 GetInt64(IDataRecord rec, string fldname)
    {
      return GetInt64(rec, rec.GetOrdinal(fldname));
    }
  
  }
}
