#Alpha Protocol Example NET

This example was generated in Visual Studio 2008 with C# targeting NET 3.5

Example has since been upgraded to Visual Studio 2013 with C# targeting NET 4.5.1

##Files

`frmMain.cs`

- This is the GUI. It handles the settings and button clicks.

`AlphaExamples.cs`

- This contains the actual protocol examples called by the GUI.
- Each example builds a protocol string, performs any optional pocsag/checksum, sends to comm wrapper.

`CommunicationWrapper.cs`

- This takes the protocol strings built in the examples and sends it via the selected method.

##Protocols

There a two protocol options provided via chechboxes in the GUI

- Pocsag, This a way to represent ascii control codes as printable character combinations.
   Bascially any ascii character with a code less than 32(dec) is replaced with a right bracket "]"
   and the original code plus 32(dec). For example 0x01 is replaced with 0x5D 0x21 (or 93 33 dec).

- Checksums, Alpha protocol provides a provision for a simple checksum. A routine is provided.


