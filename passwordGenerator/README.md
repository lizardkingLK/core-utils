# Password Generator

A password generator to quickly generate passwords.

For publish and installation steps below, set the location to passwordGenerator.Program directory as the root before execution.

## Publish

```
dotnet pack
```

## Installation

```
dotnet tool install --global --add-source .\nupkg passwordgenerator.program
```

## Run

### Requirements

- dotnet sdk version 9.0
- text editor

### Options

```
help        = [-[h|-help]]
interactive = [-[i|-interactive]]
numeric     = [-[n|-numeric]]
lowercase   = [-[l|-lowercase]]
uppercase   = [-[u|-uppercase]]
symbolic    = [-[s|-symbolic]]
count       = [-[c|-count]] (16-128)
```

### Inside Dev Environment

```
dotnet run -f net9.0 -- [above_options]
```

### Using Installed Binary

```
pass [above_options]
```

### Examples

```
pass
pass -h
pass -i
pass -nlcus 20
pass -n -l -u -s -c 20
```

## Uninstall

```
dotnet tool uninstall -g passwordgenerator.program
```
