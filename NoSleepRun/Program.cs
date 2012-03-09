﻿/**
  * NoSleepRun
  * Program.cs
  *
  * Copyright 2012 Javier Campos
  * http://www.javiercampos.info
  * 
  * Licensed under the Apache License, Version 2.0 (the "License");
  * you may not use this file except in compliance with the License.
  * 
  * You may obtain a copy of the License at
  * 
  * http://www.apache.org/licenses/LICENSE-2.0
  * 
  * Unless required by applicable law or agreed to in writing, software
  * distributed under the License is distributed on an "AS IS" BASIS,
  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  * See the License for the specific language governing permissions and
  * limitations under the License.    
  *    
  **/

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace NoSleepRun
{
  class NativeMethods
  {
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern ExecutionState SetThreadExecutionState(
    ExecutionState flags);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

    [Flags]
    public enum ExecutionState : uint
    {
      EsSystemRequired = 0x00000001,
      EsDisplayRequired = 0x00000002,
      EsContinuous = 0x80000000
    }
  }

  /// <summary>
  /// Main program
  /// </summary>
  class Program
  {
    private static bool Is32Bit { get { return IntPtr.Size == 4; } }


    /// <summary>
    /// Prevents the system powerdown.
    /// </summary>
    static void PreventSystemPowerdown()
    {
      NativeMethods.SetThreadExecutionState(NativeMethods.ExecutionState.EsSystemRequired | NativeMethods.ExecutionState.EsContinuous);
    }

    /// <summary>
    /// Allows the system powerdown.
    /// </summary>
    static void AllowSystemPowerdown()
    {
      NativeMethods.SetThreadExecutionState(NativeMethods.ExecutionState.EsContinuous);
    }

    private static bool _silent;
    static void WriteLine(string format, params object[] parms)
    {
      if (!_silent)
        Console.WriteLine(format, parms);
    }

    /// <summary>
    /// Prints the usage and exit.
    /// </summary>
    /// <param name="additionalinfo">Additional information</param>
    /// <param name="exit">If set to <c>true</c>, exit, else just print usage.</param>
    /// <param name="exitCode">Exit code to send to OS</param>
    static void PrintUsageAndExit(string additionalinfo = null, bool exit = true, int exitCode = -1)
    {
      var addUsageParameters = string.Empty;
      var addUsageRemarks = string.Empty;

      if (Is32Bit)
      {
        addUsageParameters += Strings.Add64BitParameter;
        addUsageRemarks += Strings.Add64BitRemark;
      }
      WriteLine(Strings.UsageString, addUsageParameters, addUsageRemarks);

      if (!string.IsNullOrEmpty(additionalinfo))
        WriteLine(additionalinfo);
      if (exit)
        Environment.Exit(exitCode);
    }

    /// <summary>
    /// Main entry point
    /// </summary>
    /// <param name="args">Command line args.</param>
    static void Main(string[] args)
    {
      var doneParsing = false;
      var useShellExec = false;
      var createWindow = true;
      var username = string.Empty;
      var password = default(SecureString);
      var argStrings = args.AsEnumerable();
      bool disable64bitredirection = false;

      // Parse arguments
      while (!doneParsing)
      {
        if (argStrings.Count() == 0) { PrintUsageAndExit(Strings.ErrorProcessNotSpecified); }
        var param = argStrings.First().ToLower();

        switch (param)
        {
          // Should always be the first argument to parse
          case "-x":
          case "--silent":
            _silent = true;
            break;

          case "-s":
          case "--shell":
          case "--shellexecute":
            useShellExec = true;
            break;

          case "-u":
          case "--user":
            {
              createWindow = false;
              argStrings = argStrings.Skip(1);
              var pprm = argStrings.FirstOrDefault();
              if (pprm == default(string))
              {
                PrintUsageAndExit(Strings.ErrorNoUsernameParameter);
                break;
              }
              username = pprm;
            }
            break;

          case "-p":
          case "--password":
            {
              createWindow = false;
              argStrings = argStrings.Skip(1);
              var pprm = argStrings.FirstOrDefault();
              if (pprm == default(string))
              {
                PrintUsageAndExit(Strings.ErrorNoPasswordParameter);
                break;
              }
              password = new SecureString();
              foreach (var c in pprm.ToCharArray()) password.AppendChar(c);
            }
            break;

          case "-64":
          case "--64bit":
            {
              if (Is32Bit)
                disable64bitredirection = true;
              else
                PrintUsageAndExit(string.Format(Strings.ErrorInvalidParameter, param));
            }
            break;
          default:
            doneParsing = true;
            break;
        }
        if (!doneParsing)
          argStrings = argStrings.Skip(1);
        // Assume invalid parameter
        else if (param.StartsWith("-"))
          PrintUsageAndExit(string.Format(Strings.ErrorInvalidParameter, param));
      }

      if (argStrings.Count() == 0) { PrintUsageAndExit(Strings.ErrorProcessNotSpecified); }

      // If there's a username or a password, then we can't use ShellExecute
      if (useShellExec && (!string.IsNullOrEmpty(username) || password != default(SecureString)))
      {
        PrintUsageAndExit(Strings.ErrorCantUseShellExecuteWithUsernameOrPassword);
        return;
      }
      var exe = argStrings.First();
      var prm = argStrings.Skip(1);
      var parameters = string.Empty;

      // For some reason, Process.Start doesn't allow for arg parameters... which is a pity since we have to mangle with quotes and spaces again
      if (prm.Count() > 0)
        parameters = prm.Aggregate((a, b) => a + " " + (b.Replace("\"", "\"\"").Contains(" ") ? string.Format("\"{0}\"", b) : b));

      var ptr64BitRedirection = IntPtr.Zero;
      var exitCode = -1;
      try
      {
        // Requests preventing system sleep/powerdown
        PreventSystemPowerdown();

        var pi = new ProcessStartInfo(exe, parameters) { UseShellExecute = useShellExec, CreateNoWindow = !createWindow, UserName = username, Password = password };

        // If specified, try to disable Wow64Node redirection, this allows 64 bit processes to be called from a 32 bit process and access 64 bit resources
        if (disable64bitredirection)
        {
          try
          {
            ptr64BitRedirection = new IntPtr();
            if (!NativeMethods.Wow64DisableWow64FsRedirection(ref ptr64BitRedirection))
              throw new NotSupportedException(Strings.WarningCouldntSet64bitRedirection);
          }
          catch { ptr64BitRedirection = IntPtr.Zero; WriteLine(Strings.WarningCouldntSet64bitRedirection); }
        }
        var p = Process.Start(pi);
        p.WaitForExit();

        WriteLine("{0}: {1}", Assembly.GetExecutingAssembly().GetName().Name.ToLower(), string.Format(Strings.ProgramFinishedWithExitCodeParameter, p.ExitCode));

        // Pass the exit code along to the OS
        exitCode = p.ExitCode;
      }
      catch (Exception ex)
      {
        WriteLine("{0}: {1}", Assembly.GetExecutingAssembly().GetName().Name.ToLower(), string.Format(Strings.ErrorCreatingProcessExceptionMessageParameter, ex.Message));
      }
      finally
      {
        // If redirected, revert Wow64Node redirection
        if (ptr64BitRedirection != IntPtr.Zero)
        {
          try
          {
            if (!NativeMethods.Wow64RevertWow64FsRedirection(ptr64BitRedirection))
              throw new NotSupportedException(Strings.WarningCouldntRevert64bitRedirection);
          }
          catch { WriteLine(Strings.WarningCouldntRevert64bitRedirection); }
        }
        // Allow system sleep/powerdown. This is a per-process request, so we should not need to re-enable it, but just in case
        AllowSystemPowerdown();
      }
      Environment.Exit(exitCode);
    }
  }
}
