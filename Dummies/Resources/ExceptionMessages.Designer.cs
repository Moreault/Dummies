﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToolBX.Dummies.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ToolBX.Dummies.Resources.ExceptionMessages", typeof(ExceptionMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to Amount must be greater than zero but its value was {0}.
        /// </summary>
        internal static string CannotCreateNegativeOrZeroObjects {
            get {
                return ResourceManager.GetString("CannotCreateNegativeOrZeroObjects", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} on {1} is not public.
        /// </summary>
        internal static string FieldIsNotPublic {
            get {
                return ResourceManager.GetString("FieldIsNotPublic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type &apos;{0}&apos; could not be instantiated automatically. Use a Customization to guide Dummy..
        /// </summary>
        internal static string Instantiation {
            get {
                return ResourceManager.GetString("Instantiation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Max must be greater than min but their values were {0} and {1}.
        /// </summary>
        internal static string MaxMustBeGreaterThanMin {
            get {
                return ResourceManager.GetString("MaxMustBeGreaterThanMin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression type is not supported.
        /// </summary>
        internal static string MemberExpressionUnsupported {
            get {
                return ResourceManager.GetString("MemberExpressionUnsupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no field or property {0} on type {1}.
        /// </summary>
        internal static string NoFieldOrPropertyWithName {
            get {
                return ResourceManager.GetString("NoFieldOrPropertyWithName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property {0} on {1} must have a public set or init accessor.
        /// </summary>
        internal static string PropertyMustBeMutable {
            get {
                return ResourceManager.GetString("PropertyMustBeMutable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string StartDateMustBeEarlier {
            get {
                return ResourceManager.GetString("StartDateMustBeEarlier", resourceCulture);
            }
        }
    }
}
