# NoSleepRun

Simple console utility to prevent Windows from sleeping while running a process

## Usage

    nosleeprun [OPTIONS] process [args]


## Options

* **--silent**: silent output, do not print anything
* **-s|--shellexecute**: use ShellExecute
* **-u|--username &lt;username&gt;**: execute as &lt;username&gt;
* **-p|--password &lt;password&gt;**: use &lt;password&gt;
* **-64|--64bit**: disable syswow64 redirection

## Remarks:
 - Username and Password cannot be used in conjunction with ShellExecute. This is a Windows restriction.
 - Use the -64 switch only if you need to run a 64 bit process which needs access to 64 bit Windows system libraries.
  - This option is only available if the binary is compiled as AnyCPU and executed on a 64bit system

Report bugs to <jcl@javiercampos.info>