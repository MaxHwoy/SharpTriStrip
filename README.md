# SharpTriStrip
> A C# port of NvTriStrip library that contains most of its features.

While native C++ NvTriStrip is great, this library is oriented for C#.

## Targets
netstandard2.1 (supports Unity and UnityEditor, too).

## Features
- Generates strips from arbitrary geometry using index buffer given.
- Flexibly optimizes for post TnL vertex caches (16 on GeForce1/2, 24 on GeForce3, supports custom cache sizes as well).
- Can stitch together strips using degenerate triangles, or not.
- Can output lists instead of strips.
- Can optionally throw excessively small strips into a list instead.
- Can remap indices to improve spatial locality in your vertex buffers.

## Usage
This implementation includes simpliest way to convert array of triangle indices into triangle strips.

```csharp
using SharpTriStrip;
// ...
public PrimitiveGroup[] ToTriangleStrips(ushort[] indexBuffer, bool validateStrips)
{
    var triStrip = new TriStrip(); // create new class instance

    triStrip.DisableRestart(); // we want separate strips, so restart is not needed
    triStrip.SetCacheSize(16); // GeForce1/2 vertex cache size is 16
    triStrip.SetListsOnly(false); // we want separate strips, not optimized list
    triStrip.SetMinStripSize(0); // minimum triangle count in a strip is 0
    triStrip.SetStitchStrips(false); // don't stitch strips into one huge strip

    if (triStrip.GenerateStrips(indexBuffer, out var result, validateStrips))
    {
        return result; // if strips were generated and validated correctly, return
    }

    return null; // if something went wrong, return null (or throw instead)
}
```
The XML documentation for the library is included as well, each method, class, and property has a description attached to it.

## Testing
Library contains one more project SharpTriStrip.Tests attached to it, which is a unit test project with 8 test index buffers included.

## Motivation
While working on one of my projects I needed a library that could convert triangle buffers into triangle strips, however, there was no such C# libraries that I could find at that moment. The only options left was either use NvTriStrip with P/Invoke or attempt to port it to C#, and, of course, I decided to choose the later one.

## License
This code is under a BSD license. This essentially means you can freely use it as long as you include the copyright statements as attribution. See the license file for details.
