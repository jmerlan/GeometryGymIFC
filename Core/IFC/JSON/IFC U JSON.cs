// MIT License
// Copyright (c) 2016 Geometry Gym Pty Ltd

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial 
// portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public partial class IfcUnitaryEquipment : IfcEnergyConversionDevice
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcUnitaryEquipmentTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcUnitaryEquipmentType : IfcEnergyConversionDeviceType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcUnitaryEquipmentTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcUnitAssignment : BaseClassIfc
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcUnit>(obj.GetValue("Units", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddUnit(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			JArray array = new JArray();
			foreach(int unit in mUnits)
				array.Add(mDatabase[unit].getJson(this, processed));
			obj["Units"] = array;
		}
	}

}
