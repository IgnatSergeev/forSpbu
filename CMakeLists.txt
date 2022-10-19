cmake_minimum_required(VERSION 3.23)
project(forSpbu C)

set(CMAKE_C_STANDARD 99)

add_executable(forSpbu Modules/temp.c Modules/customArrayStack.c)
