dotnet build --sc -c Release

mkdir -p obj/Flatpak/State
mkdir -p obj/Flatpak/Build

flatpak-builder --user --install --force-clean \
                --state-dir=obj/Flatpak/State  \
                obj/Flatpak/Build              \
                dev.bedsteler20.SharpGames.yaml
                
rm -rf ./obj/AppImage/AppDir || true
cp -r bin/Release/net7.0/linux-x64 ./obj/AppImage/AppDir
mkdir -p obj/AppImage/Build

appimage-builder --appdir=obj/AppImage/AppDir \
                 --build-dir=obj/AppImage/Build

mv *.AppImage bin/