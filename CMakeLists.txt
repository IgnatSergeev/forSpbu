cmake_minimum_required(VERSION 3.20.2)
project(forSpbu C)

set(CMAKE_C_STANDARD 99)

add_executable(forSpbu Modules/ListModule/customList.c KR5/rewrite.c)
