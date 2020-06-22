#!/bin/env bash

BIN=./bin
rm -rf ${BIN} && mkdir -p ${BIN}
cp ./scripts/*.lua ${BIN}


LUA=lua-5.3.5
if [[ ! -d ${LUA} ]]; then
    wget https://www.lua.org/ftp/${LUA}.tar.gz && tar -xvzf ${LUA}.tar.gz && rm ${LUA}.tar.gz
    
    cd ${LUA} \
    && sed -i 's/TO_LIB= liblua.a/TO_LIB= liblua.a liblua.so/g' Makefile \
    && sed -i 's/LUA_A=	liblua.a/LUA_A=	liblua.a\nLUA_SO= liblua.so/g' src/Makefile \
    && sed -i 's/ALL_T= $(LUA_A) $(LUA_T) $(LUAC_T)/ALL_T= $(LUA_A) $(LUA_SO) $(LUA_T) $(LUAC_T)/g' src/Makefile \
    && sed -i 's/ALL_A= $(LUA_A)/ALL_A= $(LUA_A) $(LUA_SO)/g' src/Makefile \
    && sed -i 's/\t$(RANLIB) $@/\t$(RANLIB) $@\n\n$(LUA_SO): $(BASE_O)\n\t$(CC) -o $@ -shared -fPIC $(LDFLAGS) $(BASE_O)/g' src/Makefile \
    && make linux install MYCFLAGS=-fPIC INSTALL_TOP=.. \
    && cd ..
fi
cp ${LUA}/lib/*.so ${BIN}


HOSTCS=./hostcs
if [[ ! -f "${HOSTCS}/$(basename ${HOSTCS}).csproj" ]]; then
    dotnet new console -o hostcs \
    && sed -i 's/  <\/PropertyGroup>/    <PublishTrimmed>true<\/PublishTrimmed>\n    <PublishReadyToRun>true<\/PublishReadyToRun>\n    <PublishSingleFile>true<\/PublishSingleFile>\n    <RuntimeIdentifier>linux-x64<\/RuntimeIdentifier>\n  <\/PropertyGroup>/g' "${HOSTCS}/$(basename ${HOSTCS}).csproj" \
    && rm -rf ${HOSTCS}/Program.cs
fi
dotnet publish -o ${BIN} ${HOSTCS}
