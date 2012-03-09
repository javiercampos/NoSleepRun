﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoSleepRun {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NoSleepRun.Strings", typeof(Strings).Assembly);
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
        ///   Looks up a localized string similar to   -64|--64bit         disable syswow64 redirection
        ///
        ///.
        /// </summary>
        internal static string Add64BitParameter {
            get {
                return ResourceManager.GetString("Add64BitParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - Use the -64 switch only if you need to run a 64 bit process which needs access to 64 bit Windows system libraries
        ///.
        /// </summary>
        internal static string Add64BitRemark {
            get {
                return ResourceManager.GetString("Add64BitRemark", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: can&apos;t use --shellexecute along with a username or password.
        /// </summary>
        internal static string ErrorCantUseShellExecuteWithUsernameOrPassword {
            get {
                return ResourceManager.GetString("ErrorCantUseShellExecuteWithUsernameOrPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error creating process
        ///exception message: {0}.
        /// </summary>
        internal static string ErrorCreatingProcessExceptionMessageParameter {
            get {
                return ResourceManager.GetString("ErrorCreatingProcessExceptionMessageParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: unrecognized parameter: &apos;{0}&apos;.
        /// </summary>
        internal static string ErrorInvalidParameter {
            get {
                return ResourceManager.GetString("ErrorInvalidParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: used parameter --password without supplying a password.
        /// </summary>
        internal static string ErrorNoPasswordParameter {
            get {
                return ResourceManager.GetString("ErrorNoPasswordParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: used parameter --username without supplying a username.
        /// </summary>
        internal static string ErrorNoUsernameParameter {
            get {
                return ResourceManager.GetString("ErrorNoUsernameParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: process not specified.
        /// </summary>
        internal static string ErrorProcessNotSpecified {
            get {
                return ResourceManager.GetString("ErrorProcessNotSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to process finished with exit code &apos;{0}&apos;.
        /// </summary>
        internal static string ProgramFinishedWithExitCodeParameter {
            get {
                return ResourceManager.GetString("ProgramFinishedWithExitCodeParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: nosleeprun [OPTIONS] process [args]
        ///This tool prevents Windows from sleeping or hibernating while executing command
        ///  
        ///  --silent            silent output, do not print anything.
        ///  -s|--shellexecute   use ShellExecute
        ///  -u|--username       execute as &lt;username&gt;
        ///  -p|--password       use &lt;password&gt;
        ///{0}      
        ///Remarks:
        /// - Username and Password cannot be used in conjunction with ShellExecute. This is a Windows restriction.
        ///{1}
        ///Report bugs to &lt;jcl@javiercampos.info&gt;
        ///.
        /// </summary>
        internal static string UsageString {
            get {
                return ResourceManager.GetString("UsageString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to warning: couldn&apos;t revert 64 bit redirection, calling thread might be unstable.
        /// </summary>
        internal static string WarningCouldntRevert64bitRedirection {
            get {
                return ResourceManager.GetString("WarningCouldntRevert64bitRedirection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to warning: couldn&apos;t disable 64 bit redirection, trying unredirected start.
        /// </summary>
        internal static string WarningCouldntSet64bitRedirection {
            get {
                return ResourceManager.GetString("WarningCouldntSet64bitRedirection", resourceCulture);
            }
        }
    }
}