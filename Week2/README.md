## Task 1

### Task 1.A

The terminal output after ```cifuzz init```.

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/3f0329f6-f61c-4472-9fa5-21ce777b1994)

Changed code lines in ```cifuzz.yaml``` file:
```yaml
build-system: cmake #Since we use cmake, cmake has been set.
timeout: 5m #The maximum time has set to 5 minutes.
use-sandbox: false #to run fuzz tests unsandboxed.
style: plain #Style for CI Fuzz.
```

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/3ac3cd7b-55b1-437b-bb2d-5f46ec6a1185)


### Task 1.B

The empty ```test1.cpp``` file is created. 

Commands: 
```console
mkdir test
cifuzz create -o test/test1.cpp
```

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/9d3e2004-86ae-47c6-b871-ee9203e2dee8)

### Task 1.C

1. Changed the CMakeFile.txt to below:

```
cmake_minimum_required(VERSION 3.16)
project(cmake_example) #update project name

set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

enable_testing()

find_package(cifuzz NO_SYSTEM_ENVIRONMENT_PATH)
enable_fuzz_testing()

add_subdirectory(src)

add_executable(${PROJECT_NAME} main.cpp ) #this executes main.cpp file present in the root project directory
target_link_libraries(${PROJECT_NAME} PRIVATE calculator)

add_fuzz_test(test1 test/test1.cpp)
target_link_libraries(test1 PRIVATE calculator)
```

2. Command used to run the fuzzer: ```cifuzz run test1```
3. Screenshot:
![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/3b3f15ed-1734-41d1-a6f5-5f9d19aca185)



## Task 2

### Task 2.A

The content of the fuzz file:

```cpp
#include <src/calculator.h>
#include <assert.h>
#include <stdio.h>
#include <cifuzz/cifuzz.h>
#include <fuzzer/FuzzedDataProvider.h>

FUZZ_TEST_SETUP() {
  // Perform any one-time setup required by the FUZZ_TEST function.
}

FUZZ_TEST(const uint8_t *data, size_t size) {
  FuzzedDataProvider fuzzed_data(data, size);
  int num1 = fuzzed_data.ConsumeIntegral<int8_t>();
  int num2 = fuzzed_data.ConsumeIntegral<int8_t>();
  char op = fuzzed_data.ConsumeChar();

  calculator(num1, op, num2);
}
```

Screenshot of the fuzz test file:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/e4912d42-deda-42a3-b82d-4d613dfc953f)


### Task 2.B

12 test cases have been entered. 

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/b1b7f75b-17ca-4c5e-b5e1-c3e9f66bf3f6)

The unit test cases included cover some of the calculator function's fundamental functionality and error scenarios including fundamental mathematical operations, handling of division by zero errors, and a situation with an incorrect operator. The edge cases and large numbers are also tested in the test cases.
But the checks for operator combinations or more complex expressions has not been included. Also, there are no cases to test the the performance and efficiency. 

The test cases are not enough for a large project but this is OK for this biginner level testing according to my point of view. 

### Task 2.C

The command is: ```cifuzz run test1```

Output after running the fuzz test for 5 minutes:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/fc348715-28ae-4721-b955-fb3c6dd4ea67)

No findings are available. 

### Task 2.D

Command: ```cifuzz coverage test1```

Terminal output:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/c0398c42-7702-4350-82cc-0f438c070fba)

Code-coverage report:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/91b376d5-8b70-4ef3-a2c6-6b813233b50e)

For each source file that was tested, it divides the coverage into many categories, such as functions, lines, and branches. All functions in ```calculator.cpp``` and ```test1.cpp``` files were exercised and each individual line has been executed at least once. For the code to be thoroughly tested and for possible flaws to be found during fuzz testing, high coverage in both production and test code is essential. Achieving great coverage, however, does not ensure that all potential problems have been identified.


## Task 3

### Task 3.A

Found this bug: ```gracious_jackal```

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/b249705a-0177-4a60-99d9-575097c88cf9)

The error: ```\[gracious_jackal] undefined behavior in calculator (src/calculator.cpp:37:7)``` is in the below command. 
![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/d54e24b4-f382-40ef-9baa-d5e5de832b26)

### Task 3.B

