#include <assert.h>
#include "../src/calculator.h"
#include <cifuzz/cifuzz.h>
#include <fuzzer/FuzzedDataProvider.h>
#include <cstdint>
#include <cstddef>
#include <string>

FUZZ_TEST_SETUP() {
  // Perform any one-time setup required by the FUZZ_TEST function.
}

FUZZ_TEST(const uint8_t *data, size_t size) {
  
  FuzzedDataProvider fuzzed_data(data, size);
  float num1 = fuzzed_data.ConsumeFloatingPoint();
  float num2 = fuzzed_data.ConsumeFloatingPoint();
  
  std::string randomString = fuzzed_data.ConsumeRandomLengthString();
  

  char op;

  if (!randomString.empty()) {
      op = randomString.at(0); 
  } else {

  }

  calculator(num1, op, num2);
}
