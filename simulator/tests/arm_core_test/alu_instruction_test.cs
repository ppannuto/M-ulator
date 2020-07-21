/*------------------------------------------------------------------------------
 * alu_instruction_test.c
 *
 * Branden Ghena (brghena@umich.edu)
 * 07/10/2013
 *
 * Tests all ALU instructions on the Cortex M0 processor
 *----------------------------------------------------------------------------*/

/* adcs_test
 *
 * Tests the Add with carry and condition set instruction
 */

# Test additions without carry
.thumb_func
adcs_test:
    # Zero out Carry bit (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0 + 1 (+0) = 1
    LDR  R0, =0
    LDR  R1, =1
    LDR  R2, =1
    ADCS R0, R0, R1
    CMP  R0, R2
    BNE  adcs_test_led_lbl

    # Zero out Carry bit (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x7FFFFFFF + 1 (+0) = 0x80000000
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =1
    LDR  R2, =0x80000000
    ADCS R0, R0, R1
    CMP  R0, R2
    BNE  adcs_test_led_lbl

# Test additions with carry
    # Set carry bit to 1 (NZCV = 0010)
    LDR R0, =1
    LDR R1, =0
    CMP R0, R1

    # 0 + 1 (+1) = 2
    LDR  R0, =0
    LDR  R1, =1
    LDR  R2, =2
    ADCS R0, R0, R1
    CMP  R0, R2
    BNE  adcs_test_led_lbl

    # Set carry bit to 1 (NZCV = 0010)
    LDR R0, =1
    LDR R1, =0
    CMP R0, R1

    # 0x7FFFFFFF + 1 (+1) = 0x80000001
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =1
    LDR  R2, =0x80000001
    ADCS R0, R0, R1
    CMP  R0, R2
    BNE  adcs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x7FFFFFFF + 1 (+0) => NZCV = 1001
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =1
    ADCS R0, R0, R1
    BPL  adcs_test_led_lbl
    BEQ  adcs_test_led_lbl
    BCS  adcs_test_led_lbl
    BVC  adcs_test_led_lbl

    # 1 + -1 (+0) => NZCV = 0110
    LDR  R0, =1
    LDR  R1, =-1
    ADCS R0, R0, R1
    BMI  adcs_test_led_lbl
    BNE  adcs_test_led_lbl
    BCC  adcs_test_led_lbl
    BVS  adcs_test_led_lbl

# Test Complete
    B adcs_test_rtn_lbl

    # Failure
adcs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
adcs_test_rtn_lbl
    BX LR


/* add_imm_test
 *
 * Tests the Add immediate instruction. Only works with the SP or PC as the
 *  additive register
 */

.thumb_func
add_imm_test:
# Test addition & condition flags
    # Set NZCV bits (NZCV = 0110)
    LDR R0, =0
    LDR R1, =0
    CMP R0, R1

    # SP + 12 = 0x00002000
    # (SP = 0x00001FF4)
    LDR R0, =0
    LDR R2, =0x00002000
    ADD R0, SP, #12

    # SP + 4 => should not affect NZCV
    BMI add_imm_test_led_lbl
    BNE add_imm_test_led_lbl
    BCC add_imm_test_led_lbl
    BVS add_imm_test_led_lbl

    # Test that value is correct
    CMP R0, R2
    BNE add_imm_test_led_lbl

# Test Complete
    B add_imm_test_rtn_lbl

    # Failure
add_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
add_imm_test_rtn_lbl
    BX LR


/* adds_test
 *
 * Tests the Add with condition set instruction
 */

.thumb_func
adds_test:
# Test additions
    # 0 + 1 = 1
    LDR  R0, =0
    LDR  R1, =1
    LDR  R2, =1
    ADDS R0, R0, R1
    CMP  R0, R2
    BNE  adds_test_led_lbl

    # 0x7FFFFFFF + 1 = 0x80000000
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =1
    LDR  R2, =0x80000000
    ADDS R0, R0, R1
    CMP  R0, R2
    BNE  adds_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x7FFFFFFF + 1 => NZCV = 1001
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =1
    ADDS R0, R0, R1
    BPL  adds_test_led_lbl
    BEQ  adds_test_led_lbl
    BCS  adds_test_led_lbl
    BVC  adds_test_led_lbl

    # 1 + -1 => NZCV = 0110
    LDR  R0, =1
    LDR  R1, =-1
    ADDS R0, R0, R1
    BMI  adds_test_led_lbl
    BNE  adds_test_led_lbl
    BCC  adds_test_led_lbl
    BVS  adds_test_led_lbl

# Test Complete
    B adds_test_rtn_lbl

    # Failure
adds_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
adds_test_rtn_lbl
    BX LR


/* adds_imm_test
 *
 * Tests the Add with immediate and condition set instruction
 */

.thumb_func
adds_imm_test:
# Test additions
    # 0 + 1 = 1
    LDR  R0, =0
    LDR  R2, =1
    ADDS R0, R0, #1
    CMP  R0, R2
    BNE  adds_imm_test_led_lbl

    # 0x7FFFFFFF + 1 = 0x80000000
    LDR  R0, =0x7FFFFFFF
    LDR  R2, =0x80000000
    ADDS R0, R0, #1
    CMP  R0, R2
    BNE  adds_imm_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x7FFFFFFF + 1 => NZCV = 1001
    LDR  R0, =0x7FFFFFFF
    ADDS R0, R0, #1
    BPL  adds_imm_test_led_lbl
    BEQ  adds_imm_test_led_lbl
    BCS  adds_imm_test_led_lbl
    BVC  adds_imm_test_led_lbl

    # 1 + -1 => NZCV = 0110
    LDR  R0, =-1
    ADDS R0, R0, #1
    BMI  adds_imm_test_led_lbl
    BNE  adds_imm_test_led_lbl
    BCC  adds_imm_test_led_lbl
    BVS  adds_imm_test_led_lbl

# Test Complete
    B adds_imm_test_rtn_lbl

    # Failure
adds_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
adds_imm_test_rtn_lbl
    BX LR


/* ands_test
 *
 * Tests the And with condition set instruction
 */

.thumb_func
ands_test:
# Test and
    # 0x00000000 & 0xFFFFFFFF = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x00000000
    ANDS R0, R0, R1
    CMP  R0, R2
    BNE  ands_test_led_lbl

    # 0xFFFFFFFF & 0xA5A5A5A5 = 0xA5A5A5A5
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xA5A5A5A5
    LDR  R2, =0xA5A5A5A5
    ANDS R0, R0, R1
    CMP  R0, R2
    BNE  ands_test_led_lbl

    # 0xFFFFFFFF & 0x5A5A5A5A = 0x5A5A5A5A
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x5A5A5A5A
    LDR  R2, =0x5A5A5A5A
    ANDS R0, R0, R1
    CMP  R0, R2
    BNE  ands_test_led_lbl

    # 0xFFFFFFFF & 0xFFFFFFFF = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0xFFFFFFFF
    ANDS R0, R0, R1
    CMP  R0, R2
    BNE  ands_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000000 & 0xFFFFFFFF => NZCV = 0100
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    ANDS R0, R0, R1
    BMI  ands_test_led_lbl
    BNE  ands_test_led_lbl
    BCS  ands_test_led_lbl
    BVS  ands_test_led_lbl

    # 0x80000000 & 0xFFFFFFFF => NZCV = 1000
    LDR  R0, =0x80000000
    LDR  R1, =0xFFFFFFFF
    ANDS R0, R0, R1
    BPL  ands_test_led_lbl
    BEQ  ands_test_led_lbl
    BCS  ands_test_led_lbl
    BVS  ands_test_led_lbl

    # 0x7FFFFFFF & 0x7FFFFFFF => NZCV = 0000
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =0x7FFFFFFF
    ANDS R0, R0, R1
    BMI  ands_test_led_lbl
    BEQ  ands_test_led_lbl
    BCS  ands_test_led_lbl
    BVS  ands_test_led_lbl

# Test Complete
    B ands_test_rtn_lbl

    # Failure
ands_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
ands_test_rtn_lbl
    BX LR


/* asrs_test
 *
 * Tests the Arithmetic Shift Right with condition set instruction
 */

.thumb_func
asrs_test:
# Test right shift
    # 0x00000080 >> 4 = 0x00000008
    LDR  R0, =0x00000080
    LDR  R1, =4
    LDR  R2, =0x00000008
    ASRS R0, R0, R1
    CMP  R0, R2
    BNE  asrs_test_led_lbl

    # 0x80000000 >> 3 = 0xF0000000
    LDR  R0, =0x80000000
    LDR  R1, =3
    LDR  R2, =0xF0000000
    ASRS R0, R0, R1
    CMP  R0, R2
    BNE  asrs_test_led_lbl

    # 0x7FFFFFFF >> 32 = 0
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =32
    LDR  R2, =0
    ASRS R0, R0, R1
    CMP  R0, R2
    BNE  asrs_test_led_lbl

    # 0xFFFFFFFF >> 32 = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =32
    LDR  R2, =0xFFFFFFFF
    ASRS R0, R0, R1
    CMP  R0, R2
    BNE  asrs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000001 >> 1 => NZCV = 0110
    LDR  R0, =0x00000001
    LDR  R1, =1
    ASRS R0, R0, R1
    BMI  asrs_test_led_lbl
    BNE  asrs_test_led_lbl
    BCC  asrs_test_led_lbl
    BVS  asrs_test_led_lbl

    # 0x80000000 >> 1 => NZCV = 1000
    LDR  R0, =0x80000000
    LDR  R1, =1
    ASRS R0, R0, R1
    BPL  asrs_test_led_lbl
    BEQ  asrs_test_led_lbl
    BCS  asrs_test_led_lbl
    BVS  asrs_test_led_lbl

# Test Complete
    B asrs_test_rtn_lbl

    # Failure
asrs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
asrs_test_rtn_lbl
    BX LR


/* asrs_imm_test
 *
 * Tests the Arithmetic Shift Right with immediate and condition set
 *  instruction
 */

.thumb_func
asrs_imm_test:
# Test right shift
    # 0x00000080 >> 4 = 0x00000008
    LDR  R0, =0x00000080
    LDR  R2, =0x00000008
    ASRS R0, R0, #4
    CMP  R0, R2
    BNE  asrs_imm_test_led_lbl

    # 0x80000000 >> 3 = 0xF0000000
    LDR  R0, =0x80000000
    LDR  R2, =0xF0000000
    ASRS R0, R0, #3
    CMP  R0, R2
    BNE  asrs_imm_test_led_lbl

    # 0x7FFFFFFF >> 32 = 0
    LDR  R0, =0x7FFFFFFF
    LDR  R2, =0
    ASRS R0, R0, #32
    CMP  R0, R2
    BNE  asrs_imm_test_led_lbl

    # 0xFFFFFFFF >> 32 = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R2, =0xFFFFFFFF
    ASRS R0, R0, #32
    CMP  R0, R2
    BNE  asrs_imm_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000001 >> 1 => NZCV = 0110
    LDR  R0, =0x00000001
    ASRS R0, R0, #1
    BMI  asrs_imm_test_led_lbl
    BNE  asrs_imm_test_led_lbl
    BCC  asrs_imm_test_led_lbl
    BVS  asrs_imm_test_led_lbl

    # 0x80000000 >> 1 => NZCV = 1000
    LDR  R0, =0x80000000
    ASRS R0, R0, #1
    BPL  asrs_imm_test_led_lbl
    BEQ  asrs_imm_test_led_lbl
    BCS  asrs_imm_test_led_lbl
    BVS  asrs_imm_test_led_lbl

# Test Complete
    B asrs_imm_test_rtn_lbl

    # Failure
asrs_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
asrs_imm_test_rtn_lbl
    BX LR


/* bics_test
 *
 * Tests the Bit Clear with condition set instruction
 */

.thumb_func
bics_test:
# Test Clearing Bits
    # 0xFFFFFFFF clear(0xFFFFFFFF) = 0x00000000
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x00000000
    BICS R0, R0, R1
    CMP  R0, R2
    BNE  bics_test_led_lbl

    # 0xFFFFFFFF clear(0x00000000) = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000000
    LDR  R2, =0xFFFFFFFF
    BICS R0, R0, R1
    CMP  R0, R2
    BNE  bics_test_led_lbl

    # 0x00000000 clear(0xFFFFFFFF) = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x00000000
    BICS R0, R0, R1
    CMP  R0, R2
    BNE  bics_test_led_lbl

    # 0x00000000 clear(0x00000000) = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    BICS R0, R0, R1
    CMP  R0, R2
    BNE  bics_test_led_lbl

    # 0xA5A5A5A5 clear(0xF0F0F0F0) = 0x05050505
    LDR  R0, =0xA5A5A5A5
    LDR  R1, =0xF0F0F0F0
    LDR  R2, =0x05050505
    BICS R0, R0, R1
    CMP  R0, R2
    BNE  bics_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0xFFFFFFFF clear(0x7FFFFFFF) => NZCV = 1000
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x7FFFFFFF
    BICS R0, R0, R1
    BPL  bics_test_led_lbl
    BEQ  bics_test_led_lbl
    BCS  bics_test_led_lbl
    BVS  bics_test_led_lbl

    # 0x05050505 clear(0x0F0F0F0F) => NZCV = 0100
    LDR  R0, =0x05050505
    LDR  R1, =0x0F0F0F0F
    BICS R0, R0, R1
    BMI  bics_test_led_lbl
    BNE  bics_test_led_lbl
    BCS  bics_test_led_lbl
    BVS  bics_test_led_lbl

# Test Complete
    B bics_test_rtn_lbl

    # Failure
bics_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
bics_test_rtn_lbl
    BX LR


/* cmn_test
 *
 * Tests the Compare Negative instruction
 */

.thumb_func
cmn_test:
# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x7FFFFFFF cmn 1 => NZCV = 1001
    LDR R0, =0x7FFFFFFF
    LDR R1, =1
    CMN R0, R1
    BPL cmn_test_led_lbl
    BEQ cmn_test_led_lbl
    BCS cmn_test_led_lbl
    BVC cmn_test_led_lbl

    # 1 cmn -1 => NZCV = 0110
    LDR R0, =1
    LDR R1, =-1
    CMN R0, R1
    BMI cmn_test_led_lbl
    BNE cmn_test_led_lbl
    BCC cmn_test_led_lbl
    BVS cmn_test_led_lbl

# Test Complete
    B cmn_test_rtn_lbl

    # Failure
cmn_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
cmn_test_rtn_lbl
    BX LR


/* eors_test
 *
 * Tests the Exclusive Or with condition set instruction
 */

.thumb_func
eors_test:
# Test xor
    # 0x00000000 ^ 0xFFFFFFFF = 0xFFFFFFFF
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0xFFFFFFFF
    EORS R0, R0, R1
    CMP  R0, R2
    BNE  eors_test_led_lbl

    # 0x00000000 ^ 0x00000000 = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    EORS R0, R0, R1
    CMP  R0, R2
    BNE  eors_test_led_lbl

    # 0xFFFFFFFF ^ 0x00000000 = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000000
    LDR  R2, =0xFFFFFFFF
    EORS R0, R0, R1
    CMP  R0, R2
    BNE  eors_test_led_lbl

    # 0xFFFFFFFF ^ 0xFFFFFFFF = 0x00000000
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x00000000
    EORS R0, R0, R1
    CMP  R0, R2
    BNE  eors_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0xFFFFFFFF ^ 0xFFFFFFFF => NZCV = 0100
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xFFFFFFFF
    EORS R0, R0, R1
    BMI  eors_test_led_lbl
    BNE  eors_test_led_lbl
    BCS  eors_test_led_lbl
    BVS  eors_test_led_lbl

    # 0x00000000 ^ 0xFFFFFFFF => NZCV = 1000
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    EORS R0, R0, R1
    BPL  eors_test_led_lbl
    BEQ  eors_test_led_lbl
    BCS  eors_test_led_lbl
    BVS  eors_test_led_lbl

    # 0x00000001 ^ 0x00000000 => NZCV = 0000
    LDR  R0, =0x00000001
    LDR  R1, =0x00000000
    EORS R0, R0, R1
    BMI  eors_test_led_lbl
    BEQ  eors_test_led_lbl
    BCS  eors_test_led_lbl
    BVS  eors_test_led_lbl

# Test Complete
    B eors_test_rtn_lbl

    # Failure
eors_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
eors_test_rtn_lbl
    BX LR


/* lsls_test
 *
 * Tests the Logical Shift Left with condition set instruction
 */

.thumb_func
lsls_test:
# Test left shift
    # 0x00000001 << 4 = 0x00000010
    LDR  R0, =0x00000001
    LDR  R1, =4
    LDR  R2, =0x00000010
    LSLS R0, R0, R1
    CMP  R0, R2
    BNE  lsls_test_led_lbl

    # 0xFFFFFFFF << 1 = 0xFFFFFFFE
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =1
    LDR  R2, =0xFFFFFFFE
    LSLS R0, R0, R1
    CMP  R0, R2
    BNE  lsls_test_led_lbl

    # 0xFFFFFFFF << 32 = 0
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =32
    LDR  R2, =0
    LSLS R0, R0, R1
    CMP  R0, R2
    BNE  lsls_test_led_lbl

    # 0x00000001 << 0 = 0x00000001
    LDR  R0, =0x00000001
    LDR  R1, =0
    LDR  R2, =0x00000001
    LSLS R0, R0, R1
    CMP  R0, R2
    BNE  lsls_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x80000000 << 1 => NZCV = 0110
    LDR  R0, =0x80000000
    LDR  R1, =1
    LSLS R0, R0, R1
    BMI  lsls_test_led_lbl
    BNE  lsls_test_led_lbl
    BCC  lsls_test_led_lbl
    BVS  lsls_test_led_lbl

    # 0x40000000 << 1 => NZCV = 1000
    LDR  R0, =0x40000000
    LDR  R1, =1
    LSLS R0, R0, R1
    BPL  lsls_test_led_lbl
    BEQ  lsls_test_led_lbl
    BCS  lsls_test_led_lbl
    BVS  lsls_test_led_lbl

# Test Complete
    B lsls_test_rtn_lbl

    # Failure
lsls_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
lsls_test_rtn_lbl
    BX LR


/* lsls_imm_test
 *
 * Tests the Logical Shift Left with immediate and condition set instruction
 */

.thumb_func
lsls_imm_test:
# Test left shift
    # 0x00000001 << 4 = 0x00000010
    LDR  R0, =0x00000001
    LDR  R2, =0x00000010
    LSLS R0, R0, #4
    CMP  R0, R2
    BNE  lsls_imm_test_led_lbl

    # 0xFFFFFFFF << 1 = 0xFFFFFFFE
    LDR  R0, =0xFFFFFFFF
    LDR  R2, =0xFFFFFFFE
    LSLS R0, R0, #1
    CMP  R0, R2
    BNE  lsls_imm_test_led_lbl

    # 0xFFFFFFFF << 32 = 0x80000000
    LDR  R0, =0xFFFFFFFF
    LDR  R2, =0x80000000
    LSLS R0, R0, #31
    CMP  R0, R2
    BNE  lsls_imm_test_led_lbl

    # 0x00000001 << 0 = 0x00000001
    LDR  R0, =0x00000001
    LDR  R2, =0x00000001
    LSLS R0, R0, #0
    CMP  R0, R2
    BNE  lsls_imm_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x80000000 << 1 => NZCV = 0110
    LDR  R0, =0x80000000
    LSLS R0, R0, #1
    BMI  lsls_imm_test_led_lbl
    BNE  lsls_imm_test_led_lbl
    BCC  lsls_imm_test_led_lbl
    BVS  lsls_imm_test_led_lbl

    # 0x40000000 << 1 => NZCV = 1000
    LDR  R0, =0x40000000
    LSLS R0, R0, #1
    BPL  lsls_imm_test_led_lbl
    BEQ  lsls_imm_test_led_lbl
    BCS  lsls_imm_test_led_lbl
    BVS  lsls_imm_test_led_lbl

# Test Complete
    B lsls_imm_test_rtn_lbl

    # Failure
lsls_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
lsls_imm_test_rtn_lbl
    BX LR


/* lsrs_test
 *
 * Tests the Logical Shift Right with condition set instruction
 */

.thumb_func
lsrs_test:
# Test right shift
    # 0x00000080 >> 4 = 0x00000008
    LDR  R0, =0x00000080
    LDR  R1, =4
    LDR  R2, =0x00000008
    LSRS R0, R0, R1
    CMP  R0, R2
    BNE  lsrs_test_led_lbl

    # 0x80000000 >> 3 = 0x10000000
    LDR  R0, =0x80000000
    LDR  R1, =3
    LDR  R2, =0x10000000
    LSRS R0, R0, R1
    CMP  R0, R2
    BNE  lsrs_test_led_lbl

    # 0xFFFFFFFF >> 32 = 0
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =32
    LDR  R2, =0
    LSRS R0, R0, R1
    CMP  R0, R2
    BNE  lsrs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000001 >> 1 => NZCV = 0110
    LDR  R0, =0x00000001
    LDR  R1, =1
    LSRS R0, R0, R1
    BMI  lsrs_test_led_lbl
    BNE  lsrs_test_led_lbl
    BCC  lsrs_test_led_lbl
    BVS  lsrs_test_led_lbl

    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0xFFFFFFFF >> 0 => NZCV = 1000
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0
    LSRS R0, R0, R1
    BPL  lsrs_test_led_lbl
    BEQ  lsrs_test_led_lbl
    BCS  lsrs_test_led_lbl
    BVS  lsrs_test_led_lbl

    # 0x80000000 >> 1 => NZCV = 0000
    LDR  R0, =0x80000000
    LDR  R1, =1
    LSRS R0, R0, R1
    BMI  lsrs_test_led_lbl
    BEQ  lsrs_test_led_lbl
    BCS  lsrs_test_led_lbl
    BVS  lsrs_test_led_lbl

# Test Complete
    B lsrs_test_rtn_lbl

    # Failure
lsrs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
lsrs_test_rtn_lbl
    BX LR


/* lsrs_imm_test
 *
 * Tests the Logical Shift Right with immediate and condition set instruction
 */

.thumb_func
lsrs_imm_test:
# Test right shift
    # 0x00000080 >> 4 = 0x00000008
    LDR  R0, =0x00000080
    LDR  R2, =0x00000008
    LSRS R0, R0, #4
    CMP  R0, R2
    BNE  lsrs_imm_test_led_lbl

    # 0x80000000 >> 3 = 0x10000000
    LDR  R0, =0x80000000
    LDR  R2, =0x10000000
    LSRS R0, R0, #3
    CMP  R0, R2
    BNE  lsrs_imm_test_led_lbl

    # 0xFFFFFFFF >> 32 = 0
    LDR  R0, =0xFFFFFFFF
    LDR  R2, =0
    LSRS R0, R0, #32
    CMP  R0, R2
    BNE  lsrs_imm_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000001 >> 1 => NZCV = 0110
    LDR  R0, =0x00000001
    LDR  R1, =1
    LSRS R0, R0, #1
    BMI  lsrs_imm_test_led_lbl
    BNE  lsrs_imm_test_led_lbl
    BCC  lsrs_imm_test_led_lbl
    BVS  lsrs_imm_test_led_lbl

    # 0x80000000 >> 1 => NZCV = 0000
    LDR  R0, =0x80000000
    LSRS R0, R0, #1
    BMI  lsrs_imm_test_led_lbl
    BEQ  lsrs_imm_test_led_lbl
    BCS  lsrs_imm_test_led_lbl
    BVS  lsrs_imm_test_led_lbl

# Test Complete
    B lsrs_imm_test_rtn_lbl

    # Failure
lsrs_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
lsrs_imm_test_rtn_lbl
    BX LR


/* mov_test
 *
 * Tests the Move instruction
 */

.thumb_func
mov_test:
# Test move
    # R1 = 0xFFFFFFFF
    LDR R0, =0xFFFFFFFF
    LDR R1, =0x00000000
    MOV R1, R0
    CMP R0, R1
    BNE mov_test_led_lbl

    # R1 = 0x00000000
    LDR R0, =0x00000000
    LDR R1, =0xFFFFFFFF
    MOV R1, R0
    CMP R0, R1
    BNE mov_test_led_lbl

    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Condition bits should be unaffected
    LDR R0, =0x00000000
    LDR R1, =0xFFFFFFFF
    MOV R1, R0
    BMI lsrs_imm_test_led_lbl
    BEQ lsrs_imm_test_led_lbl
    BCS lsrs_imm_test_led_lbl
    BVS lsrs_imm_test_led_lbl

# Test Complete
    B mov_test_rtn_lbl

    # Failure
mov_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
mov_test_rtn_lbl
    BX LR


/* movs_test
 *
 * Tests the Move with condition set instruction
 */

.thumb_func
movs_test:
# Test move
    # R1 = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000000
    MOVS R1, R0
    CMP  R0, R1
    BNE  movs_test_led_lbl

    # R1 = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    MOVS R1, R0
    CMP  R0, R1
    BNE  movs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # R1 = 0x00000000 => NZCV = 0100
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    MOVS R1, R0
    BMI  movs_test_led_lbl
    BNE  movs_test_led_lbl
    BCS  movs_test_led_lbl
    BVS  movs_test_led_lbl

    # R1 = 0x80000000 => NZCV = 1000
    LDR  R0, =0x80000000
    LDR  R1, =0x00000000
    MOVS R1, R0
    BPL  movs_test_led_lbl
    BEQ  movs_test_led_lbl
    BCS  movs_test_led_lbl
    BVS  movs_test_led_lbl

    # R1 = 0x7FFFFFFF => NZCV = 0000
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =0xFFFFFFFF
    MOVS R1, R0
    BMI  movs_test_led_lbl
    BEQ  movs_test_led_lbl
    BCS  movs_test_led_lbl
    BVS  movs_test_led_lbl

# Test Complete
    B movs_test_rtn_lbl

    # Failure
movs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
movs_test_rtn_lbl
    BX LR


/* movs_imm_test
 *
 * Tests the Move with immediate and condition set instruction
 */

.thumb_func
movs_imm_test:
# Test move
    # R1 = 0x00000000
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000000
    MOVS R0, #0
    CMP  R0, R1
    BNE  movs_imm_test_led_lbl

    # R1 = 0x000000FF
    LDR  R0, =0x00000000
    LDR  R1, =0x000000FF
    MOVS R0, #0xFF
    CMP  R0, R1
    BNE  movs_imm_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # R1 = 0x00000000 => NZCV = 0100
    LDR  R1, =0xFFFFFFFF
    MOVS R1, #0
    BMI  movs_imm_test_led_lbl
    BNE  movs_imm_test_led_lbl
    BCS  movs_imm_test_led_lbl
    BVS  movs_imm_test_led_lbl

    # R1 = 0x000000FF => NZCV = 0000
    LDR  R1, =0xFFFFFFFF
    MOVS R1, #0xFF
    BMI  movs_imm_test_led_lbl
    BEQ  movs_imm_test_led_lbl
    BCS  movs_imm_test_led_lbl
    BVS  movs_imm_test_led_lbl

# Test Complete
    B movs_imm_test_rtn_lbl

    # Failure
movs_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
movs_imm_test_rtn_lbl
    BX LR


/* muls_test
 *
 * Tests the Multiply with condition set instruction
 */

.thumb_func
muls_test:
# Test multiply
    # 0x00000002 * 0x00000005 = 0x0000000A
    LDR  R0, =0x00000002
    LDR  R1, =0x00000005
    LDR  R2, =0x0000000A
    MULS R0, R1, R0
    CMP  R0, R2
    BNE  muls_test_led_lbl

    # 0x00000000 * 0x00000000 = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    MULS R0, R1, R0
    CMP  R0, R2
    BNE  muls_test_led_lbl

    # 0xFFFFFFFF * 0xFFFFFFFF = 0x00000001 (0xFFFFFFFE00000001)
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x00000001
    MULS R0, R1, R0
    CMP  R0, R2
    BNE  muls_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0xFFFFFFFF * 0x00000001 => NZCV = 1000
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000001
    MULS R0, R1, R0
    BPL  muls_test_led_lbl
    BEQ  muls_test_led_lbl
    BCS  muls_test_led_lbl
    BVS  muls_test_led_lbl

    # 0xFFFFFFFF * 0x00000000 => NZCV = 0100
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000000
    MULS R0, R1, R0
    BMI  muls_test_led_lbl
    BNE  muls_test_led_lbl
    BCS  muls_test_led_lbl
    BVS  muls_test_led_lbl

    # 0x00000001 * 0x00000001 => NZCV = 0000
    LDR  R0, =0x00000001
    LDR  R1, =0x00000001
    MULS R0, R1, R0
    BMI  muls_test_led_lbl
    BEQ  muls_test_led_lbl
    BCS  muls_test_led_lbl
    BVS  muls_test_led_lbl

# Test Complete
    B muls_test_rtn_lbl

    # Failure
muls_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
muls_test_rtn_lbl
    BX LR


/* mvns_test
 *
 * Tests the Move Not with condition set instruction
 */

.thumb_func
mvns_test:
# Test move not
    # R0 = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x00000000
    MVNS R0, R1
    CMP  R0, R2
    BNE  mvns_test_led_lbl

    # R0 = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000000
    LDR  R2, =0xFFFFFFFF
    MVNS R0, R1
    CMP  R0, R2
    BNE  mvns_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # R0 = 0x00000000 => NZCV = 0100
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xFFFFFFFF
    MVNS R0, R1
    BMI  mvns_test_led_lbl
    BNE  mvns_test_led_lbl
    BCS  mvns_test_led_lbl
    BVS  mvns_test_led_lbl

    # R0 = 0x80000000 => NZCV = 1000
    LDR  R0, =0x00000000
    LDR  R1, =0x7FFFFFFF
    MVNS R0, R1
    BPL  mvns_test_led_lbl
    BEQ  mvns_test_led_lbl
    BCS  mvns_test_led_lbl
    BVS  mvns_test_led_lbl

    # R0 = 0x7FFFFFFF => NZCV = 0000
    LDR  R0, =0x00000000
    LDR  R1, =0x80000000
    MVNS R0, R1
    BMI  mvns_test_led_lbl
    BEQ  mvns_test_led_lbl
    BCS  mvns_test_led_lbl
    BVS  mvns_test_led_lbl

# Test Complete
    B mvns_test_rtn_lbl

    # Failure
mvns_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
mvns_test_rtn_lbl
    BX LR


/* orrs_test
 *
 * Tests the Or with condition set instruction
 */

.thumb_func
orrs_test:
# Test or
    # 0x00000000 | 0xFFFFFFFF = 0xFFFFFFFF
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0xFFFFFFFF
    ORRS R0, R0, R1
    CMP  R0, R2
    BNE  orrs_test_led_lbl

    # 0x00000000 | 0x00000000 = 0x00000000
    LDR  R0, =0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    ORRS R0, R0, R1
    CMP  R0, R2
    BNE  orrs_test_led_lbl

    # 0xFFFFFFFF | 0x00000000 = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0x00000000
    LDR  R2, =0xFFFFFFFF
    ORRS R0, R0, R1
    CMP  R0, R2
    BNE  orrs_test_led_lbl

    # 0xFFFFFFFF | 0xFFFFFFFF = 0xFFFFFFFF
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0xFFFFFFFF
    ORRS R0, R0, R1
    CMP  R0, R2
    BNE  orrs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000000 | 0x00000000 => NZCV = 0100
    LDR  R0, =0x00000000
    LDR  R1, =0x00000000
    ORRS R0, R0, R1
    BMI  orrs_test_led_lbl
    BNE  orrs_test_led_lbl
    BCS  orrs_test_led_lbl
    BVS  orrs_test_led_lbl

    # 0x00000000 | 0xFFFFFFFF => NZCV = 1000
    LDR  R0, =0x00000000
    LDR  R1, =0xFFFFFFFF
    ORRS R0, R0, R1
    BPL  orrs_test_led_lbl
    BEQ  orrs_test_led_lbl
    BCS  orrs_test_led_lbl
    BVS  orrs_test_led_lbl

    # 0x00000001 | 0x00000000 => NZCV = 0000
    LDR  R0, =0x00000001
    LDR  R1, =0x00000000
    ORRS R0, R0, R1
    BMI  orrs_test_led_lbl
    BEQ  orrs_test_led_lbl
    BCS  orrs_test_led_lbl
    BVS  orrs_test_led_lbl

# Test Complete
    B orrs_test_rtn_lbl

    # Failure
orrs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
orrs_test_rtn_lbl
    BX LR


/* rev_test
 *
 * Tests the Reverse instruction
 */

.thumb_func
rev_test:
# Test reverse
    # rev(0xDEADBEEF) = 0xEFBEADDE
    LDR R0, =0xDEADBEEF
    LDR R2, =0xEFBEADDE
    REV R0, R0
    CMP R0, R2
    BNE rev_test_led_lbl

    # rev(0x00000000) = 0x00000000
    LDR R0, =0x00000000
    LDR R2, =0x00000000
    REV R0, R0
    CMP R0, R2
    BNE rev_test_led_lbl

    # rev(0xFFFFFFFF) = 0xFFFFFFFF
    LDR R0, =0xFFFFFFFF
    LDR R2, =0xFFFFFFFF
    REV R0, R0
    CMP R0, R2
    BNE rev_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Flags should be unaffected
    LDR R0, =0x00000000
    LDR R2, =0x00000000
    REV R0, R0
    BMI rev_test_led_lbl
    BEQ rev_test_led_lbl
    BCS rev_test_led_lbl
    BVS rev_test_led_lbl

# Test Complete
    B rev_test_rtn_lbl

    # Failure
rev_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
rev_test_rtn_lbl
    BX LR


/* rev16_test
 *
 * Tests the Reverse Halfword instruction
 */

.thumb_func
rev16_test:
# Test reverse
    # rev(0xDEADBEEF) = 0xADDEEFBE
    LDR   R0, =0xDEADBEEF
    LDR   R2, =0xADDEEFBE
    REV16 R0, R0
    CMP   R0, R2
    BNE   rev16_test_led_lbl

    # rev(0x00000000) = 0x00000000
    LDR   R0, =0x00000000
    LDR   R2, =0x00000000
    REV16 R0, R0
    CMP   R0, R2
    BNE   rev16_test_led_lbl

    # rev(0xFFFFFFFF) = 0xFFFFFFFF
    LDR   R0, =0xFFFFFFFF
    LDR   R2, =0xFFFFFFFF
    REV16 R0, R0
    CMP   R0, R2
    BNE   rev16_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Flags should be unaffected
    LDR   R0, =0x00000000
    LDR   R2, =0x00000000
    REV16 R0, R0
    BMI   rev16_test_led_lbl
    BEQ   rev16_test_led_lbl
    BCS   rev16_test_led_lbl
    BVS   rev16_test_led_lbl

# Test Complete
    B rev16_test_rtn_lbl

    # Failure
rev16_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
rev16_test_rtn_lbl
    BX LR


/* revsh_test
 *
 * Tests the Reverse Signed Halfword instruction
 */

.thumb_func
revsh_test:
# Test reverse
    # rev(0xDEADBEEF) = 0xFFFFEFBE
    LDR   R0, =0xDEADBEEF
    LDR   R2, =0xFFFFEFBE
    REVSH R0, R0
    CMP   R0, R2
    BNE   revsh_test_led_lbl

    # rev(0xFFFFFF7F) = 0x00007FFF
    LDR   R0, =0xFFFFFF7F
    LDR   R2, =0x00007FFF
    REVSH R0, R0
    CMP   R0, R2
    BNE   revsh_test_led_lbl

    # rev(0x00000000) = 0x00000000
    LDR   R0, =0x00000000
    LDR   R2, =0x00000000
    REV16 R0, R0
    CMP   R0, R2
    BNE   revsh_test_led_lbl

    # rev(0xFFFFFFFF) = 0xFFFFFFFF
    LDR   R0, =0xFFFFFFFF
    LDR   R2, =0xFFFFFFFF
    REV16 R0, R0
    CMP   R0, R2
    BNE   revsh_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Flags should be unaffected
    LDR   R0, =0x00000000
    LDR   R2, =0x00000000
    REV16 R0, R0
    BMI   revsh_test_led_lbl
    BEQ   revsh_test_led_lbl
    BCS   revsh_test_led_lbl
    BVS   revsh_test_led_lbl

# Test Complete
    B revsh_test_rtn_lbl

    # Failure
revsh_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
revsh_test_rtn_lbl
    BX LR


/* rors_test
 *
 * Tests the Rotate Right with condition set instruction
 */

.thumb_func
rors_test:
# Test right rotate
    # 0xDEADBEEF rot 4 = 0xFDEADBEE
    LDR  R0, =0xDEADBEEF
    LDR  R1, =4
    LDR  R2, =0xFDEADBEE
    RORS R0, R0, R1
    CMP  R0, R2
    BNE  rors_test_led_lbl

    # 0x80000000 rot 3 = 0x10000000
    LDR  R0, =0x80000000
    LDR  R1, =3
    LDR  R2, =0x10000000
    RORS R0, R0, R1
    CMP  R0, R2
    BNE  rors_test_led_lbl

    # 0xDEADBEEF rot 32 = 0xDEADBEEF
    LDR  R0, =0xDEADBEEF
    LDR  R1, =32
    LDR  R2, =0xDEADBEEF
    RORS R0, R0, R1
    CMP  R0, R2
    BNE  rors_test_led_lbl

    # 0x80000000 rot 35 = 0x10000000
    LDR  R0, =0x80000000
    LDR  R1, =35
    LDR  R2, =0x10000000
    RORS R0, R0, R1
    CMP  R0, R2
    BNE  rors_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0xFFFFFFFF rot 1 => NZCV = 1010
    LDR  R0, =0xFFFFFFFF
    LDR  R1, =1
    RORS R0, R0, R1
    BPL  rors_test_led_lbl
    BEQ  rors_test_led_lbl
    BCC  rors_test_led_lbl
    BVS  rors_test_led_lbl

    # 0x00000000 rot 31 => NZCV = 0100
    LDR  R0, =0x00000000
    LDR  R1, =31
    RORS R0, R0, R1
    BMI  rors_test_led_lbl
    BNE  rors_test_led_lbl
    BCS  rors_test_led_lbl
    BVS  rors_test_led_lbl

    # 0x80000000 rot 1 => NZCV = 0000
    LDR  R0, =0x80000000
    LDR  R1, =1
    RORS R0, R0, R1
    BMI  rors_test_led_lbl
    BEQ  rors_test_led_lbl
    BCS  rors_test_led_lbl
    BVS  rors_test_led_lbl

# Test Complete
    B rors_test_rtn_lbl

    # Failure
rors_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
rors_test_rtn_lbl
    BX LR


/* rsbs_test
 *
 * Tests the Reverse Subtract with condition set instruction
 */

.thumb_func
rsbs_test:
# Test reverse subtract
    # 0 - 1 = 0xFFFFFFFF
    LDR  R0, =1
    LDR  R2, =0xFFFFFFFF
    RSBS R0, R0, #0
    CMP  R0, R2
    BNE  rsbs_test_led_lbl

    # 0 - 0xFFFFFFFF = 1
    LDR  R0, =0xFFFFFFFF
    LDR  R2, =1
    RSBS R0, R0, #0
    CMP  R0, R2
    BNE  rsbs_test_led_lbl

    # 0 - 0x80000000 = 0x80000000
    LDR  R0, =0x80000000
    LDR  R2, =0x80000000
    RSBS R0, R0, #0
    CMP  R0, R2
    BNE  rsbs_test_led_lbl

    # 0 - 0x7FFFFFFF = 0x80000001
    LDR  R0, =0x7FFFFFFF
    LDR  R2, =0x80000001
    RSBS R0, R0, #0
    CMP  R0, R2
    BNE  rsbs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0 - 1 => NZCV = 1000
    LDR  R0, =1
    RSBS R0, R0, #0
    BPL  rsbs_test_led_lbl
    BEQ  rsbs_test_led_lbl
    BCS  rsbs_test_led_lbl
    BVS  rsbs_test_led_lbl

    # 0 - 0 => NZCV = 0110
    LDR  R0, =0
    RSBS R0, R0, #0
    BMI  rsbs_test_led_lbl
    BNE  rsbs_test_led_lbl
    BCC  rsbs_test_led_lbl
    BVS  rsbs_test_led_lbl

    # 0 - 0x80000000 => NZCV = 1001
    LDR  R0, =0x80000000
    RSBS R0, R0, #0
    BPL  rsbs_test_led_lbl
    BEQ  rsbs_test_led_lbl
    BCS  rsbs_test_led_lbl
    BVC  rsbs_test_led_lbl

    # 0 - 0xFFFFFFFF => NZCV = 0000
    LDR  R0, =0xFFFFFFFF
    RSBS R0, R0, #0
    BMI  rsbs_test_led_lbl
    BEQ  rsbs_test_led_lbl
    BCS  rsbs_test_led_lbl
    BVS  rsbs_test_led_lbl

# Test Complete
    B rsbs_test_rtn_lbl

    # Failure
rsbs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
rsbs_test_rtn_lbl
    BX LR


/* sbcs_test
 *
 * Tests the Subtract with carry and condition set instruction
 *
 * Note: The GenericUserGuide is incorrect as to the functionality of this
 *  instruction. It is actually implemented as Rn-Rm-1+Carry. So a further one
 *  is deducted UNLESS the carry bit is SET.
 *
 * SBCS R0, R1, R2 -> R0 := R1 - R2 + C - 1
 */

.thumb_func
sbcs_test:
# Test subractions without carry
    # Zero out Carry bit (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0 - 1 (-1+0) = -2
    LDR  R0, =0
    LDR  R1, =1
    LDR  R2, =-2
    SBCS R0, R0, R1
    CMP  R0, R2
    BNE  sbcs_test_led_lbl

    # Zero out Carry bit (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x80000000 - 1 (-1+0) = 0x7FFFFFFE
    LDR  R0, =0x80000000
    LDR  R1, =1
    LDR  R2, =0x7FFFFFFE
    SBCS R0, R0, R1
    CMP  R0, R2
    BNE  sbcs_test_led_lbl

    # Zero out Carry bit (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000008 - 1 (-1+0) = 0x00000006
    LDR  R0, =0x00000008
    LDR  R1, =1
    LDR  R2, =0x00000006
    SBCS R0, R0, R1
    CMP  R0, R2
    BNE  sbcs_test_led_lbl

# Test subractions with carry
    # Set carry bit to 1 (NZCV = 0010)
    LDR R0, =1
    LDR R1, =0
    CMP R0, R1

    # 0 - 1 (-1+1) = -1
    LDR  R0, =0
    LDR  R1, =1
    LDR  R2, =-1
    SBCS R0, R0, R1
    CMP  R0, R2
    BNE  sbcs_test_led_lbl

    # Set carry bit to 1 (NZCV = 0010)
    LDR R0, =1
    LDR R1, =0
    CMP R0, R1

    # 0x80000000 - 1 (-1+1) = 0x7FFFFFFF
    LDR  R0, =0x80000000
    LDR  R1, =1
    LDR  R2, =0x7FFFFFFF
    SBCS R0, R0, R1
    CMP  R0, R2
    BNE  sbcs_test_led_lbl

    # Set carry bit to 1 (NZCV = 0010)
    LDR R0, =1
    LDR R1, =0
    CMP R0, R1

    # 0x00000008 - 1 (-1+1) = 0x00000007
    LDR  R0, =0x00000008
    LDR  R1, =1
    LDR  R2, =0x00000007
    SBCS R0, R0, R1
    CMP  R0, R2
    BNE  sbcs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x80000000 - 1 (-1+0) => NZCV = 0011
    LDR  R0, =0x80000000
    LDR  R1, =1
    SBCS R0, R0, R1
    BMI  sbcs_test_led_lbl
    BEQ  sbcs_test_led_lbl
    BCC  sbcs_test_led_lbl
    BVC  sbcs_test_led_lbl

    # Set carry bit to 1 (NZCV = 0010)
    LDR R0, =1
    LDR R1, =0
    CMP R0, R1

    # 1 - 1 (-1+1) => NZCV = 0110
    LDR  R0, =1
    LDR  R1, =1
    SBCS R0, R0, R1
    BMI  sbcs_test_led_lbl
    BNE  sbcs_test_led_lbl
    BCC  sbcs_test_led_lbl
    BVS  sbcs_test_led_lbl

    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # -2 - -2 (-1+0) => NZCV = 1000
    LDR  R0, =-2
    LDR  R1, =-2
    SBCS R0, R0, R1
    BPL  sbcs_test_led_lbl
    BEQ  sbcs_test_led_lbl
    BCS  sbcs_test_led_lbl
    BVS  sbcs_test_led_lbl

# Test Complete
    B sbcs_test_rtn_lbl

    # Failure
sbcs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
sbcs_test_rtn_lbl
    BX LR


/* sub_imm_test
 *
 * Tests the Subtract with immediate instruction
 *
 * Note: this test utilizes the ADD command which has previously been tested
 */

.thumb_func
sub_imm_test:
# Test subtraction & condition flags
    # Set NZCV bits (NZCV = 0110)
    LDR R0, =0
    LDR R1, =0
    CMP R0, R1

    # SP - 4 = 0x00001FF0
    # (SP = 0x00001FF4)
    LDR R2, =0x00001FF0
    SUB SP, SP, #4

    # SP - 4 => should not affect NZCV
    BMI sub_imm_test_led_lbl
    BNE sub_imm_test_led_lbl
    BCC sub_imm_test_led_lbl
    BVS sub_imm_test_led_lbl

    # Test that value is correct
    CMP SP, R2
    BNE sub_imm_test_led_lbl

    # Fix the SP
    ADD SP, SP, #4

# Test Complete
    B sub_imm_test_rtn_lbl

    # Failure
sub_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
sub_imm_test_rtn_lbl
    BX LR


/* subs_test
 *
 * Tests the Subtract with condition set instruction
 */

.thumb_func
subs_test:
# Test subtraction
    # 0 - 1 = -1
    LDR  R0, =0
    LDR  R1, =1
    LDR  R2, =-1
    SUBS R0, R0, R1
    CMP  R0, R2
    BNE  subs_test_led_lbl

    # 0x80000000 - 1 = 0x7FFFFFFF
    LDR  R0, =0x80000000
    LDR  R1, =1
    LDR  R2, =0x7FFFFFFF
    SUBS R0, R0, R1
    CMP  R0, R2
    BNE  subs_test_led_lbl

    # 0x7FFFFFFF - -1 = 0x80000000
    LDR  R0, =0x7FFFFFFF
    LDR  R1, =-1
    LDR  R2, =0x80000000
    SUBS R0, R0, R1
    CMP  R0, R2
    BNE  subs_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x80000000 - 1 => NZCV = 0011
    LDR  R0, =0x80000000
    LDR  R1, =1
    SUBS R0, R0, R1
    BMI  subs_test_led_lbl
    BEQ  subs_test_led_lbl
    BCC  subs_test_led_lbl
    BVC  subs_test_led_lbl

    # 1 - 1 => NZCV = 0110
    LDR  R0, =1
    LDR  R1, =1
    SUBS R0, R0, R1
    BMI  subs_test_led_lbl
    BNE  subs_test_led_lbl
    BCC  subs_test_led_lbl
    BVS  subs_test_led_lbl

    # 0 - 1 => NZCV = 1000
    LDR  R0, =0
    LDR  R1, =1
    SUBS R0, R0, R1
    BPL  subs_test_led_lbl
    BEQ  subs_test_led_lbl
    BCS  subs_test_led_lbl
    BVS  subs_test_led_lbl

# Test Complete
    B subs_test_rtn_lbl

    # Failure
subs_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
subs_test_rtn_lbl
    BX LR


/* subs_imm_test
 *
 * Tests the Subtract with immediate and condition set instruction
 */

.thumb_func
subs_imm_test:
# Test subraction
    # 0 - 1 = -1
    LDR  R0, =0
    LDR  R2, =-1
    SUBS R0, R0, #1
    CMP  R0, R2
    BNE  subs_imm_test_led_lbl

    # 0x80000000 - 1 = 0x7FFFFFFF
    LDR  R0, =0x80000000
    LDR  R2, =0x7FFFFFFF
    SUBS R0, R0, #1
    CMP  R0, R2
    BNE  subs_imm_test_led_lbl

    # 0x7FFFFFFF - 0 = 0x7FFFFFFF
    LDR  R0, =0x7FFFFFFF
    LDR  R2, =0x7FFFFFFF
    SUBS R0, R0, #0
    CMP  R0, R2
    BNE  subs_imm_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x80000000 - 1 => NZCV = 0011
    LDR  R0, =0x80000000
    SUBS R0, R0, #1
    BMI  subs_imm_test_led_lbl
    BEQ  subs_imm_test_led_lbl
    BCC  subs_imm_test_led_lbl
    BVC  subs_imm_test_led_lbl

    # 1 - 1 => NZCV = 0110
    LDR  R0, =1
    SUBS R0, R0, #1
    BMI  subs_imm_test_led_lbl
    BNE  subs_imm_test_led_lbl
    BCC  subs_imm_test_led_lbl
    BVS  subs_imm_test_led_lbl

    # 0 - 1 => NZCV = 1000
    LDR  R0, =0
    SUBS R0, R0, #1
    BPL  subs_imm_test_led_lbl
    BEQ  subs_imm_test_led_lbl
    BCS  subs_imm_test_led_lbl
    BVS  subs_imm_test_led_lbl

# Test Complete
    B subs_imm_test_rtn_lbl

    # Failure
subs_imm_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
subs_imm_test_rtn_lbl
    BX LR


/* sxtb_test
 *
 * Tests the Sign Extend Byte instruction
 */

.thumb_func
sxtb_test:
# Test sign extend
    # sxtb(0x000000FF) -> 0xFFFFFFFF
    LDR  R1, =0x000000FF
    LDR  R2, =0xFFFFFFFF
    SXTB R0, R1
    CMP  R0, R2
    BNE  sxtb_test_led_lbl

    # sxtb(0x00000000) -> 0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    SXTB R0, R1
    CMP  R0, R2
    BNE  sxtb_test_led_lbl

    # sxtb(0xFFFFFF7F) -> 0x0000007F
    LDR  R1, =0xFFFFFF7F
    LDR  R2, =0x0000007F
    SXTB R0, R1
    CMP  R0, R2
    BNE  sxtb_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Condition bits should be unaffected
    LDR  R1, =0x00000000
    SXTB R0, R1
    BMI  sxtb_test_led_lbl
    BEQ  sxtb_test_led_lbl
    BCS  sxtb_test_led_lbl
    BVS  sxtb_test_led_lbl

# Test Complete
    B sxtb_test_rtn_lbl

    # Failure
sxtb_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
sxtb_test_rtn_lbl
    BX LR


/* sxth_test
 *
 * Tests the Sign Extend Halfword instruction
 */

.thumb_func
sxth_test:
# Test sign extend
    # sxth(0x0000FFFF) -> 0xFFFFFFFF
    LDR  R1, =0x0000FFFF
    LDR  R2, =0xFFFFFFFF
    SXTH R0, R1
    CMP  R0, R2
    BNE  sxth_test_led_lbl

    # sxth(0x00000000) -> 0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    SXTH R0, R1
    CMP  R0, R2
    BNE  sxth_test_led_lbl

    # sxth(0xFFFF7FFF) -> 0x00007FFF
    LDR  R1, =0xFFFF7FFF
    LDR  R2, =0x00007FFF
    SXTH R0, R1
    CMP  R0, R2
    BNE  sxth_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Condition bits should be unaffected
    LDR  R1, =0x00000000
    SXTH R0, R1
    BMI  sxth_test_led_lbl
    BEQ  sxth_test_led_lbl
    BCS  sxth_test_led_lbl
    BVS  sxth_test_led_lbl

# Test Complete
    B sxth_test_rtn_lbl

    # Failure
sxth_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
sxth_test_rtn_lbl
    BX LR


/* uxtb_test
 *
 * Tests the Zero Extend Byte instruction
 */

.thumb_func
uxtb_text:
# Test zero extend
    # uxtb(0x000000FF) -> 0x000000FF
    LDR  R1, =0x000000FF
    LDR  R2, =0x000000FF
    UXTB R0, R1
    CMP  R0, R2
    BNE  uxtb_test_led_lbl

    # uxtb(0x00000000) -> 0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    UXTB R0, R1
    CMP  R0, R2
    BNE  uxtb_test_led_lbl

    # uxtb(0xFFFFFFFF) -> 0x000000FF
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x000000FF
    UXTB R0, R1
    CMP  R0, R2
    BNE  uxtb_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Condition bits should be unaffected
    LDR  R1, =0x00000000
    UXTB R0, R1
    BMI  uxtb_test_led_lbl
    BEQ  uxtb_test_led_lbl
    BCS  uxtb_test_led_lbl
    BVS  uxtb_test_led_lbl

# Test Complete
    B uxtb_test_rtn_lbl

    # Failure
uxtb_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
uxtb_test_rtn_lbl
    BX LR


/* uxth_test
 *
 * Tests the Zero Extend Halfword instruction
 */

.thumb_func
uxth_test:
# Test zero extend
    # uxth(0x0000FFFF) -> 0x0000FFFF
    LDR  R1, =0x0000FFFF
    LDR  R2, =0x0000FFFF
    UXTH R0, R1
    CMP  R0, R2
    BNE  uxth_test_led_lbl

    # uxth(0x00000000) -> 0x00000000
    LDR  R1, =0x00000000
    LDR  R2, =0x00000000
    UXTH R0, R1
    CMP  R0, R2
    BNE  uxth_test_led_lbl

    # uxth(0xFFFFFFFF) -> 0x0000FFFF
    LDR  R1, =0xFFFFFFFF
    LDR  R2, =0x0000FFFF
    UXTH R0, R1
    CMP  R0, R2
    BNE  uxth_test_led_lbl

# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # Condition bits should be unaffected
    LDR  R1, =0x00000000
    UXTH R0, R1
    BMI  uxth_test_led_lbl
    BEQ  uxth_test_led_lbl
    BCS  uxth_test_led_lbl
    BVS  uxth_test_led_lbl

# Test Complete
    B uxth_test_rtn_lbl

    # Failure
uxth_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
uxth_test_rtn_lbl
    BX LR


/* tst_test
 *
 * Tests the Test Bits instruction
 */

.thumb_func
tst_test:
# Test condition flags
    # Zero out NZCV bits (NZCV = 0000)
    LDR R0, =0
    LDR R1, =-1
    CMP R0, R1

    # 0x00000000 & 0xFFFFFFFF => NZCV = 0100
    LDR R0, =0x00000000
    LDR R1, =0xFFFFFFFF
    TST R0, R1
    BMI tst_test_led_lbl
    BNE tst_test_led_lbl
    BCS tst_test_led_lbl
    BVS tst_test_led_lbl

    # 0x80000000 & 0xFFFFFFFF => NZCV = 1000
    LDR R0, =0x80000000
    LDR R1, =0xFFFFFFFF
    TST R0, R1
    BPL tst_test_led_lbl
    BEQ tst_test_led_lbl
    BCS tst_test_led_lbl
    BVS tst_test_led_lbl

    # 0x7FFFFFFF & 0x7FFFFFFF => NZCV = 0000
    LDR R0, =0x7FFFFFFF
    LDR R1, =0x7FFFFFFF
    TST R0, R1
    BMI tst_test_led_lbl
    BEQ tst_test_led_lbl
    BCS tst_test_led_lbl
    BVS tst_test_led_lbl

# Test Complete
    B tst_test_rtn_lbl

    # Failure
tst_test_led_lbl
    LDR R0, =(CLK_FREQ/5)
    LDR R1, =alu_ledLoop
    BX  R1

    # Return from function
tst_test_rtn_lbl
    BX LR


/* alu_instruction_test
 *
 * Calls all ALU test functions
 */
.thumb_func
.global alu_instruction_test
alu_instruction_test:
    adcs_test();
    add_imm_test();
    adds_test();
    adds_imm_test();
    ands_test();
    asrs_test();
    asrs_imm_test();
    bics_test();
    cmn_test();
    eors_test();
    lsls_test();
    lsls_imm_test();
    lsrs_test();
    lsrs_imm_test();
    mov_test();
    movs_test();
    movs_imm_test();
    muls_test();
    mvns_test();
    orrs_test();
    rev_test();
    rev16_test();
    revsh_test();
    rors_test();
    rsbs_test();
    sbcs_test();
    sub_imm_test();
    subs_test();
    subs_imm_test();
    sxtb_test();
    sxth_test();
    uxtb_test();
    uxth_test();
    tst_test();
}


/* alu_ledLoop
 *
 * Assembly function which loops infinitely blinking
 *  an LED
 *
 * Inputs
 *   delay_count - number of iterations to wait between blinking
 */

.thumb_func
alu_ledLoop:
    B alu_ledLoop

/*
__asm void alu_ledLoop(unsigned int delay_count) {
# infinite loop
start

    # turn on the LED
    LDR R1, =LED_ON

    LDR R2, =0
loopA
    ADDS R2, #1
    CMP  R2, R0
    BNE  loopA

    # turn off the LED
    LDR R1, =LED_OFF

    LDR R2, =0
loopB
    ADDS R2, #1
    CMP  R2, R0
    BNE  loopB

    B start
}
*/
