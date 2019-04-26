# KonoeStudio.Libs.DependencyInjection
This Library is a Dependency Injection Container for Constructor Injection.
And this software is released under the MIT License, see [LICENSE](https://github.com/18konoe/KonoeStudio.Libs.DependencyInjection/blob/master/LICENSE)

You can download from nuget to use this library.
https://www.nuget.org/packages/KonoeStudio.Libs.DependencyInjection/

## Supported Framework
* .NET Framework 4.5
* .NET Standard 2.0

## Demo
There is Demo project in [KonoeStudio.Demo.DependencyInjection](https://github.com/18konoe/KonoeStudio.Demo.DependencyInjection)

## For Japanese
コンストラクタインジェクション専用のDIコンテナです。

日本語での使い方説明はブログにあります。
https://konoe.studio/how-to-use-my-di-container/

## Quick Tutorial

```csharp
// Initialization
DiVendor diVendor = new DiVendor();

// Register request interface and actual supply class (Default singleton, lazy initialized)
diVendor.Register<INoMeanInterface, NoMeanClass>();

// Register request and supply class (false = No singleton)
diVendor.Register<HaveNoMeanConstructor>(false);

// Procure registered class
HaveNoMeanConstructor haveNoMeanConstructor = diVendor.Procure<HaveNoMeanConstructor>();

// To dispose all generated instances
diVendor.Dispose();
```

## Extra Tutorial

```csharp
DiVendor diVendor = new DiVendor();
IDiArchitect diArchitect = new DiArchitect();

bool isSingleton = false;
bool isLazyinitialized = true;

// Create blueprint for extra settings
IDiBlueprint blueprint = diArchitect
    .CreateBlueprint<LiteralConstructor>(isSingleton, isLazyinitialized)
    .AppendArgumentInfo<int>(10) // You can inject Literal or existing value
    .AppendArgumentInfo<string>("TEST");

// Register with Blueprint
diVendor.Register<ILiteralConstructor, LiteralConstructor>(blueprint);
diVendor.Register<IDependedConstructor, DependedConstructor>(false);
diVendor.Register<INoMeanInterface, NoMeanClass>();

IDiBlueprint blueprintForComplex = diArchitect
    .CreateBlueprint<ComplexConstructor>(true, true)
    .AppendArgumentInfo<INoMeanInterface>(null, true) // If you inject null
    .AppendArgumentInfo<ILiteralConstructor>(null) // Only null mean auto inject
    .AppendArgumentInfo<IDependedConstructor>()
    .AppendArgumentInfo<int>(1);

diVendor.Register<IComplexConstructor, ComplexConstructor>(blueprintForComplex);

IComplexConstructor complexConstructor = diVendor.Procure<IComplexConstructor>();
```

Sample classes:
```csharp
    public interface IDependedConstructor
    {
        INoMeanInterface NoConstructor { get; }
        ILiteralConstructor LiteralConstructor { get; }
    }
    public class DependedConstructor : IDependedConstructor
    {
        public INoMeanInterface NoConstructor { get; }
        public ILiteralConstructor LiteralConstructor { get; }

        public DependedConstructor(INoMeanInterface noConstructor, ILiteralConstructor literalConstructor)
        {
            NoConstructor = noConstructor;
            LiteralConstructor = literalConstructor;
        }
    }
    public interface IComplexConstructor
    {
        INoMeanInterface NoConstructor { get; }
        ILiteralConstructor LiteralConstructor { get; }
        IDependedConstructor DependedConstructor { get; }
        int Arg1 { get; }
    }
    public class ComplexConstructor : IComplexConstructor
    {
        public INoMeanInterface NoConstructor { get; }
        public ILiteralConstructor LiteralConstructor { get; }
        public IDependedConstructor DependedConstructor { get; }
        public int Arg1 { get; }

        public ComplexConstructor(INoMeanInterface noConstructor, ILiteralConstructor literalConstructor, IDependedConstructor dependedConstructor, int arg1)
        {
            NoConstructor = noConstructor;
            LiteralConstructor = literalConstructor;
            DependedConstructor = dependedConstructor;
            Arg1 = arg1;
        }
    }
```

