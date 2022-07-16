# ChromaWrapper

A .NET wrapper for the [Razer Chroma SDK](https://developer.razer.com/works-with-chroma/).

## Features

- Provides a lightweight interface to the [C++ implementation of the Chroma SDK](https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/index.html).
- Supports the full range of device types:
  - Keyboards
  - Mice
  - Keypads
  - Headsets
  - Mousepads
  - Chroma Link devices
- Supports both static and custom effects.
- Built-in capture of SDK notifications.
- Keeps track of all created effects.

## Library requirements

- [Microsoft .NET 6.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/current/runtime) (_x64_ version)
- [Razer Synapse 3](https://www.razer.com/synapse-3)

## How to use

ChromaWrapper is available as a NuGet package, so you may add it from Visual Studio's "Manage NuGet packages" functionality.

Alternatively, you may add it from the command line by running:

```shell
dotnet add package ChromaWrapper
```

## Development requirements

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/) with the _.NET desktop development workload_ installed
- [Razer Synapse 3](https://www.razer.com/synapse-3) or the [Razer Chroma SDK](https://developer.razer.com/works-with-chroma/download/)

## Contributing

Contributions are welcome! Please read on [how to contribute](https://github.com/poveden/ChromaWrapper/blob/master/CONTRIBUTING.md).

## Code of conduct

We follow the [Contributor Covenant Code of Conduct](https://github.com/poveden/ChromaWrapper/blob/master/CODE_OF_CONDUCT.md).

## Attributions

- Heavy inspiration from the [Colore .NET library](https://github.com/chroma-sdk/Colore).
