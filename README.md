# C# Lox Interpreter

A C# implementation of the Lox language interpreter from the [*Crafting Interpreters*](https://craftinginterpreters.com/) ebook by [Bob Nystrom](https://github.com/munificent).

This is a direct port of `jlox` from the book, rewritten in C# instead of Java.

Prerequisites
To build and run this interpreter, you need the following:

.NET SDK (version 8.0 or later).

A compatible operating system (Windows or Linux).

Setup Instructions
1. Clone the Repository
First, clone this repository to your local machine:

`git clone https://github.com/etsu-algorithms/tree-walk-interpreter-infiniteloop22.git`

`cd tree-walk-interpreter-infiniteloop22`

2. Set Up the Environment
To set up the environment, use the provided setup scripts for your operating system.

Windows
1. Open PowerShell as Administrator:

2. Press Win + S, type PowerShell, right-click on Windows PowerShell, and select Run as Administrator.

3. Navigate to the repository directory:

`cd path\to\tree-walk-interpreter-infiniteloop22`

4. Run the setup script:

`.\build_windows.ps1`

This script will:

-Install the .NET SDK (if not already installed).

-Verify the installation.

You can also run the interpreter by:

`dotnet run --project .\src\Lox.csproj .\tests\example.lox`

Linux (Ubuntu 24.04 LTS)
1. Open a terminal.

2. Navigate to the repository directory:

`cd path\to\tree-walk-interpreter-infiniteloop22`
3. Make the setup script executable:

`chmod +x build_ubuntu.sh`

4. Run the setup script with sudo:

`sudo ./build_ubuntu.sh`

This script will:

-Install the .NET SDK (if not already installed).

-Verify the installation.

Build the Interpreter
Once the environment is set up, build the interpreter using the .NET CLI:

dotnet build
Run the Interpreter
You can run the interpreter in two modes: interactive mode or by executing a Lox script file.

1. Interactive Mode
To start the interpreter in interactive mode (REPL), run:

`dotnet run`

2. Run a Lox Script
To execute a Lox script from a file (e.g., example.lox), run:

`dotnet run --project .\src\Lox.csproj .\tests\example.lox`
