﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DAL.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на USING [ParkingFineDb]
        ///GO
        ///
        ///
        ///
        ///INSERT INTO [Autoes]([Id], [GovNumber], [Mark], [Model]) VALUES(1, N&apos;A437АЕ 147(RUS)&apos;, &apos;Lada&apos;, &apos;Vesta&apos;);
        ///INSERT INTO [Autoes]([Id], [GovNumber], [Mark], [Model]) VALUES(2, N&apos;Н473ЗИ 116(RUS)&apos;, &apos;Scoda&apos;, &apos;Octavia&apos;);
        ///INSERT INTO [Autoes]([Id], [GovNumber], [Mark], [Model]) VALUES(3, N&apos;548ОП 263(РУС)&apos;, &apos;Nissan&apos;, &apos;Almera&apos;);
        ///
        ///
        ///INSERT INTO [Fines]([Id], [Name], [Cost]) VALUES(1, N&apos;Парковка в неправильном месте&apos;, 1000);
        ///INSERT INTO [Fines]([Id], [Name], [Cost]) VALUES(2, N&apos;Парк [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string seed {
            get {
                return ResourceManager.GetString("seed", resourceCulture);
            }
        }
    }
}