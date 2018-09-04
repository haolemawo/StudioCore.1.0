using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infrastructure.DBBuild
{
    [TestClass]
    public class BuildEntity
    {
        [TestMethod]
        public void BuildMainAll()
        {//ȫ������(����ʹ��)
            using (var db = Utility.DBCore.GetInstance.GetDB)
            {
                var tempNamespace = Utility.Config.BuildNamespace;
                db.DbFirst.SettingClassTemplate(m => { return GetTemp; }).CreateClassFile(Factory.FileFactory.GetBuildPath, tempNamespace);
            }

        }

        private string GetTemp => @"using System;
namespace Domain.Models
{{ClassDescription}{SugarTable}
    public partial class {ClassName}:BaseDBModel
    {
        public {ClassName}(){

{Constructor}
           }

{PropertyName}
    }
}";

    }
}
