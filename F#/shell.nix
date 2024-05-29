{ pkgs ? import <nixpkgs> {} }:
pkgs.mkShell {
    name = "f#";
    packages = [ pkgs.fsautocomplete pkgs.dotnet-sdk_8 ];
    shellHook = "";
}
