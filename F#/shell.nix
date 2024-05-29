{ pkgs ? import <nixpkgs> {} }:
pkgs.mkShell {
    name = "f#";
    packages = [ pkgs.netcoredbg pkgs.fsautocomplete pkgs.dotnet-sdk_8 ];
    shellHook = "";
}
