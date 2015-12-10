//*******************************************************************
//Author: Yejoong Kim
//Description: Developed during PREv13 tape-out for verification
//*******************************************************************
#include "PREv13.h"
#include "FLSv2S_RF.h"
#include "PMUv2_RF.h"
#include "mbus.h"

#define PRE_ADDR    0x1
#define FLS_ADDR    0x4
#define NODE_A_ADDR 0x8
#define NODE_B_ADDR 0xC
#define PMU_ADDR    0xE

// FLSv2S Payloads
#define ERASE_PASS  0x55

//********************************************************************
// Global Variables
//********************************************************************
volatile uint32_t enumerated;
volatile uint32_t cyc_num;
volatile uint32_t irq_history;

volatile flsv2s_r0F_t FLSv2S_R0F_IRQ      = FLSv2S_R0F_DEFAULT;
volatile flsv2s_r12_t FLSv2S_R12_PWR_CONF = FLSv2S_R12_DEFAULT;
volatile flsv2s_r07_t FLSv2S_R07_GO       = FLSv2S_R07_DEFAULT;

volatile pmuv2_r42_t PMUv2_R42_IRQ  = PMUv2_R42_DEFAULT;

// Just for testing...
volatile uint32_t do_cycle0  = 1; // 
volatile uint32_t do_cycle1  = 1; // PMU Testing
volatile uint32_t do_cycle2  = 1; // Register test
volatile uint32_t do_cycle3  = 1; // MEM IRQ
volatile uint32_t do_cycle4  = 1; // Flash Erase
volatile uint32_t do_cycle5  = 1; // Memory Streaming 1
volatile uint32_t do_cycle6  = 1; // Memory Streaming 2
volatile uint32_t do_cycle7  = 1; // TIMER16
volatile uint32_t do_cycle8  = 1; // TIMER32
volatile uint32_t do_cycle9  = 1; // Watch-Dog
volatile uint32_t do_cycle10 = 1; // SPI
volatile uint32_t do_cycle11 = 1; // GPIO

//*******************************************************************
// INTERRUPT HANDLERS
//*******************************************************************
void handler_ext_int_0(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_1(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_2(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_3(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_4(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_5(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_6(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_7(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_8(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_9(void)  __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_10(void) __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_11(void) __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_12(void) __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_13(void) __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_14(void) __attribute__ ((interrupt ("IRQ")));
void handler_ext_int_15(void) __attribute__ ((interrupt ("IRQ")));

void handler_ext_int_0(void) { *NVIC_ICPR = (0x1 << 0); // TIMER32
    irq_history |= (0x1 << 0);
    *REG0 = 0x1000;
    *REG1 = *TIMER32_CNT;
    *REG2 = *TIMER32_STAT;
    *TIMER32_STAT = 0x0;
    arb_debug_reg (0xA0000000);
    arb_debug_reg ((0xA1 << 24) | *REG0); // 0x1000
    arb_debug_reg ((0xA1 << 24) | *REG1); // TIMER32_CNT
    arb_debug_reg ((0xA1 << 24) | *REG2); // TIMER32_STAT
    }
void handler_ext_int_1(void) { *NVIC_ICPR = (0x1 << 1); // TIMER16
    irq_history |= (0x1 << 1);
    *REG0 = 0x1001;
    *REG1 = *TIMER16_CNT;
    *REG2 = *TIMER16_STAT;
    *TIMER16_STAT = 0x0;
    arb_debug_reg (0xA0000001);
    arb_debug_reg ((0xA1 << 24) | *REG0); // 0x1001
    arb_debug_reg ((0xA1 << 24) | *REG1); // TIMER16_CNT
    arb_debug_reg ((0xA1 << 24) | *REG2); // TIMER16_STAT
    }
void handler_ext_int_2(void) { *NVIC_ICPR = (0x1 << 2); // REG0
    irq_history |= (0x1 << 2);
    arb_debug_reg (0xA0000002);
}
void handler_ext_int_3(void) { *NVIC_ICPR = (0x1 << 3); // REG1
    irq_history |= (0x1 << 3);
    arb_debug_reg (0xA0000003);
}
void handler_ext_int_4(void) { *NVIC_ICPR = (0x1 << 4); // REG2
    irq_history |= (0x1 << 4);
    arb_debug_reg (0xA0000004);
}
void handler_ext_int_5(void) { *NVIC_ICPR = (0x1 << 5); // REG3
    irq_history |= (0x1 << 5);
    arb_debug_reg (0xA0000005);
}
void handler_ext_int_6(void) { *NVIC_ICPR = (0x1 << 6); // REG4
    irq_history |= (0x1 << 6);
    arb_debug_reg (0xA0000006);
}
void handler_ext_int_7(void) { *NVIC_ICPR = (0x1 << 7); // REG5
    irq_history |= (0x1 << 7);
    arb_debug_reg (0xA0000007);
}
void handler_ext_int_8(void) { *NVIC_ICPR = (0x1 << 8); // REG6
    irq_history |= (0x1 << 8);
    arb_debug_reg (0xA0000008);
}
void handler_ext_int_9(void) { *NVIC_ICPR = (0x1 << 9); // REG7
    irq_history |= (0x1 << 9);
    arb_debug_reg (0xA0000009);
}
void handler_ext_int_10(void) { *NVIC_ICPR = (0x1 << 10); // MEM WR
    irq_history |= (0x1 << 10);
    arb_debug_reg (0xA000000A);
}
void handler_ext_int_11(void) { *NVIC_ICPR = (0x1 << 11); // MBUS_RX
    irq_history |= (0x1 << 11);
    arb_debug_reg (0xA000000B);
}
void handler_ext_int_12(void) { *NVIC_ICPR = (0x1 << 12); // MBUS_TX
    irq_history |= (0x1 << 12);
    arb_debug_reg (0xA000000C);
}
void handler_ext_int_13(void) { *NVIC_ICPR = (0x1 << 13); // MBUS_FWD
    irq_history |= (0x1 << 13);
    arb_debug_reg (0xA000000D);
}
void handler_ext_int_14(void) { *NVIC_ICPR = (0x1 << 14); // SPI
    irq_history |= (0x1 << 14);
    arb_debug_reg (0xA000000E);
}
void handler_ext_int_15(void) { *NVIC_ICPR = (0x1 << 15); // GPIO
    irq_history |= (0x1 << 15);
    arb_debug_reg (0xA000000F);
}

//*******************************************************************
// USER FUNCTIONS
//*******************************************************************
void initialization (void) {

    enumerated = 0xDEADBEEF;
    cyc_num = 0;
    irq_history = 0;

    // Set Halt
    set_halt_until_mbus_rx();

    // Enumeration
    mbus_enumerate(FLS_ADDR);
    mbus_enumerate(NODE_A_ADDR);
    mbus_enumerate(NODE_B_ADDR);
    mbus_enumerate(PMU_ADDR);

    //Set Halt
    set_halt_until_mbus_tx();
}

void pass (uint32_t id, uint32_t data) {
    arb_debug_reg (0x0EA7F00D);
    arb_debug_reg (id);
    arb_debug_reg (data);
}

void fail (uint32_t id, uint32_t data) {
    arb_debug_reg (0xDEADBEEF);
    arb_debug_reg (id);
    arb_debug_reg (data);
    *REG_CHIP_ID = 0xFFFF; // This will stop the verilog sim.
}

void cycle0 (void) {
    if (do_cycle0 == 1) {
    }
}

void cycle1 (void) {
    if (do_cycle1 == 1) {
        uint32_t idx;

        // Set Halt
        set_halt_until_mbus_rx();

        // Read Test
        for (idx=1; idx<42; idx++) {
            mbus_remote_register_write (PMU_ADDR, 0x00, idx);
            if (*REG0 == idx) {pass (0xA0, *REG0); *REG0 = 0x0;}
            else fail (0xA0, *REG0);
        }

        // Set Halt
        set_halt_until_mbus_tx();

        // Set FLS INT_SHORT_PREFIX = 10, REG_ADDR = 01
        PMUv2_R42_IRQ.INT_RPLY_REG_ADDR = 0x01;
        PMUv2_R42_IRQ.INT_RPLY_SHORT_ADDR = 0x10;
        mbus_remote_register_write(PMU_ADDR, 0x42, PMUv2_R42_IRQ.as_int);

        // Set Halt
        set_halt_until_mbus_rx();

        // Write Test
        for (idx=1; idx<42; idx++) {
            mbus_remote_register_write (PMU_ADDR, idx, idx+1);
            if (*REG1 == (idx+1)) {pass (0xA1, *REG1); *REG1 = 0x0;}
            else fail (0xA1, *REG1);
        }

        // Set Halt
        set_halt_until_mbus_tx();

        // Set FLS INT_SHORT_PREFIX = 10, REG_ADDR = 02
        PMUv2_R42_IRQ.INT_RPLY_REG_ADDR = 0x02;
        PMUv2_R42_IRQ.INT_RPLY_SHORT_ADDR = 0x10;
        mbus_remote_register_write(PMU_ADDR, 0x42, PMUv2_R42_IRQ.as_int);

        // Set Halt
        set_halt_until_mbus_rx();

        // Read Test
        for (idx=1; idx<42; idx++) {
            mbus_remote_register_write (PMU_ADDR, 0x00, idx);
            if (*REG2 == (idx+1)) {pass (0xA2, *REG2); *REG2 = 0x0;}
            else fail (0xA2, *REG2);
        }

        // Set Halt
        set_halt_until_mbus_tx();
    }
}

void cycle2 (void) {
    if (do_cycle2 == 1) {
        // Set Halt & Disable all interrupts
        set_halt_until_mbus_rx();
        
        // Copy Reg#1 in FLS and put them in Reg#0 in PRE.
        mbus_copy_registers_from_remote_to_local (FLS_ADDR, 0x01, 0x00, 0);

        // Copy Reg#1-4 in FLS and put them in Reg#0-3 in PRE.
        mbus_copy_registers_from_remote_to_local (FLS_ADDR, 0x01, 0x00, 3);
    
        // Enable MBus Tx/Rx IRQ, Enable REG IRQ (4-7)
        clear_all_pend_irq();
        *NVIC_ISER =  (0 /*GPIO*/ << 16)    | (0 /*SPI*/ << 15)     | (0 /*GOCEP*/ << 14)  | (0 /*MBUS_FWD*/ << 13) 
                    | (1 /*MBUS_TX*/ << 12) | (1 /*MBUS_RX*/ << 11) | (0 /*MEM_WR*/ << 10) | (1 /*REG7*/ << 9) 
                    | (1 /*REG6*/ << 8)     | (1 /*REG5*/ << 7)     | (1 /*REG4*/ << 6)    | (0 /*REG3*/ << 5) 
                    | (0 /*REG2*/ << 4)     | (0 /*REG1*/ << 3)     | (0 /*REG0*/ << 2)     
                    | (0 /*TIMER16*/ << 1)  | (0 /*TIMER32*/ << 0);

        // Copy Reg#1-4 in FLS and put them in Reg#4-7 in PRE. It must generate 4 IRQs due to Reg#4-7 in PRE.
        mbus_copy_registers_from_remote_to_local (FLS_ADDR, 0x01, 0x04, 3);

        // Here should be 6 IRQs: MBUS Tx/Rx and REG4-7.
        if (irq_history == ((1 << 12) | (1 << 11) | (1 << 9) | (1 << 8) | (1 << 7) | (1 << 6))) { 
               pass (0x0, irq_history); irq_history = 0; disable_all_irq(); }
        else { fail (0x0, irq_history); disable_all_irq(); }
    
        // Enable MBus Tx/Rx IRQ
        clear_all_pend_irq();
        *NVIC_ISER =  (0 /*GPIO*/ << 16)    | (0 /*SPI*/ << 15)     | (0 /*GOCEP*/ << 14)  | (0 /*MBUS_FWD*/ << 13) 
                    | (1 /*MBUS_TX*/ << 12) | (1 /*MBUS_RX*/ << 11) | (0 /*MEM_WR*/ << 10) | (0 /*REG7*/ << 9) 
                    | (0 /*REG6*/ << 8)     | (0 /*REG5*/ << 7)     | (0 /*REG4*/ << 6)    | (0 /*REG3*/ << 5) 
                    | (0 /*REG2*/ << 4)     | (0 /*REG1*/ << 3)     | (0 /*REG0*/ << 2)     
                    | (0 /*TIMER16*/ << 1)  | (0 /*TIMER32*/ << 0);
    
        // Copy Reg#1-4 in FLS and put them in Reg#4-7 in PRE. It must not generate REG IRQs
        mbus_copy_registers_from_remote_to_local (FLS_ADDR, 0x01, 0x04, 3);

        // Here should be 2 IRQs: MBUS Tx/Rx
        if (irq_history == ((1 << 12) | (1 << 11))) { 
               pass (0x1, irq_history); irq_history = 0; disable_all_irq(); }
        else { fail (0x1, irq_history); disable_all_irq(); }
    
    
        // Copy Reg#4 in FLS and put them in Reg#4. There must be no IRQ.
        mbus_copy_registers_from_remote_to_local (FLS_ADDR, 0x04, 0x04, 0);

        // Here should be 0 IRQs
        if (irq_history == 0) { 
               pass (0x2, irq_history); irq_history = 0; disable_all_irq(); }
        else { fail (0x2, irq_history); disable_all_irq(); }


        // Configure Halt Setting; Now use HALT_UNTIL_REG7
        set_halt_until_reg(7);
    
        // Enable Enable REG IRQ (4-7)
        clear_all_pend_irq();
        *NVIC_ISER =  (0 /*GPIO*/ << 16)    | (0 /*SPI*/ << 15)     | (0 /*GOCEP*/ << 14)  | (0 /*MBUS_FWD*/ << 13) 
                    | (0 /*MBUS_TX*/ << 12) | (0 /*MBUS_RX*/ << 11) | (0 /*MEM_WR*/ << 10) | (1 /*REG7*/ << 9) 
                    | (1 /*REG6*/ << 8)     | (1 /*REG5*/ << 7)     | (1 /*REG4*/ << 6)    | (0 /*REG3*/ << 5) 
                    | (0 /*REG2*/ << 4)     | (0 /*REG1*/ << 3)     | (0 /*REG0*/ << 2)     
                    | (0 /*TIMER16*/ << 1)  | (0 /*TIMER32*/ << 0);

        // Copy Reg#1-4 in FLS and put them in Reg#4-7 in PRE. It must generate 4 IRQs due to Reg#4-7 in PRE.
        mbus_copy_registers_from_remote_to_local (FLS_ADDR, 0x01, 0x04, 3);
    
        // Here should be 6 IRQs: MBUS Tx/Rx and REG4-7.
        if (irq_history == ((1 << 9) | (1 << 8) | (1 << 7) | (1 << 6))) { 
               pass (0x3, irq_history); irq_history = 0; disable_all_irq(); }
        else { fail (0x3, irq_history); disable_all_irq(); }
    
        // Configre Halt Setting; Now use HALT_UNTIL_MBUS_TX (maybe the most useful one)
        set_halt_until_mbus_tx();
    }
}

void cycle3 (void) {
    if (do_cycle3 == 1) {
        // Set Halt
        set_halt_until_mbus_rx();
        
        // Copy MEM 0x00000000 in FLS and store those in PRE's 0x00001400~
        mbus_copy_mem_from_remote_to_any_bulk (FLS_ADDR, 0x00000000, PRE_ADDR, (uint32_t *) 0x00001400, 0);

        // Copy MEM 0x00000000~0x0000003C in FLS and store those in PRE's 0x00001500~
        mbus_copy_mem_from_remote_to_any_bulk (FLS_ADDR, 0x00000000, PRE_ADDR, (uint32_t *) 0x00001500, 9);

        // Before streaming, set the pointer to start from.
        *REG_STR0_OFF = 0x00000510;
        *REG_STR1_OFF = 0x00000500;

        // Copy MEM 0x00000000 in FLS and store those in PRE's 0x00000F9C~
        mbus_copy_mem_from_remote_to_any_stream (1, FLS_ADDR, 0x00000000, PRE_ADDR, 0);

        if (*REG_STR1_OFF == 0x501) pass (0x4, *REG_STR1_OFF);
        else fail (0x4, *REG_STR1_OFF);

        // Copy MEM 0x00000000~0x0000003C in FLS and store those in PRE's 0x00000FA0~
        mbus_copy_mem_from_remote_to_any_stream (0, FLS_ADDR, 0x00000000, PRE_ADDR, 9);

        if (*REG_STR0_OFF == 0x51A) pass (0x5, *REG_STR0_OFF);
        else fail (0x5, *REG_STR0_OFF);

        // Configre Halt Setting; Now use HALT_UNTIL_MBUS_TX (maybe the most useful one)
        set_halt_until_mbus_tx();
    }
}

void cycle4 (void) {
    if (do_cycle4 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();
    
    ///////////////////////////
    // Do Flash Erase (REG0) //
    ///////////////////////////
    
        // Set FLS INT_SHORT_PREFIX = 10, REG_ADDR = 00
        FLSv2S_R0F_IRQ.INT_RPLY_REG_ADDR = 0x00;
        FLSv2S_R0F_IRQ.INT_RPLY_SHORT_ADDR = 0x10;
        mbus_remote_register_write(FLS_ADDR, 0x0F, FLSv2S_R0F_IRQ.as_int);

        // Set FLS Power Configuration (Auto On/Off)
        FLSv2S_R12_PWR_CONF.FLASH_AUTO_ON = 0x1;
        FLSv2S_R12_PWR_CONF.FLASH_AUTO_OFF = 0x1;
        FLSv2S_R12_PWR_CONF.FLASH_AUTO_USE_CUSTOM = 0x0;
        FLSv2S_R12_PWR_CONF.SEL_PWR_ON_WUP = 0x0;
        FLSv2S_R12_PWR_CONF.IRQ_PWR_ON_WUP = 0x0;
        mbus_remote_register_write(FLS_ADDR, 0x12, FLSv2S_R12_PWR_CONF.as_int);
    
        // Set HALT_UNTIL_REG0
        set_halt_until_reg(0);
    
        // Start Flash Erase
        FLSv2S_R07_GO.GO = 0x1;
        FLSv2S_R07_GO.CMD = 0x4; // Erase Flash
        FLSv2S_R07_GO.IRQ_EN = 0x1;
        FLSv2S_R07_GO.LENGTH = 0x0;
        mbus_remote_register_write(FLS_ADDR, 0x07, FLSv2S_R07_GO.as_int);

        // Check the payload
        if (*REG0 == ERASE_PASS) { pass (0x6, *REG0); }
        else { fail (0x6, *REG0); }

        // Set HALT_UNTIL_MBUS_TX
        set_halt_until_mbus_tx();

    ///////////////////////////
    // Do Flash Erase (REG4) //
    ///////////////////////////

        // Set FLS INT_SHORT_PREFIX = 10, REG_ADDR = 04
        FLSv2S_R0F_IRQ.INT_RPLY_REG_ADDR = 0x04;
        FLSv2S_R0F_IRQ.INT_RPLY_SHORT_ADDR = 0x10;
        mbus_remote_register_write(FLS_ADDR, 0x0F, FLSv2S_R0F_IRQ.as_int);

        // Set HALT_UNTIL_REG0
        set_halt_until_reg(4);
    
        // Start Flash Erase
        FLSv2S_R07_GO.GO = 0x1;
        mbus_remote_register_write(FLS_ADDR, 0x07, FLSv2S_R07_GO.as_int);

        // Check the payload
        if (*REG4 == ERASE_PASS) { pass (0x7, *REG4); }
        else { fail (0x7, *REG4); }

        // Set HALT_UNTIL_MBUS_TX
        set_halt_until_mbus_tx();

    ///////////////////////////
    // Do Flash Erase (0x1F) //
    ///////////////////////////

        // Set FLS INT_SHORT_PREFIX = 10, REG_ADDR = 04
        FLSv2S_R0F_IRQ.INT_RPLY_REG_ADDR = 0x00;
        FLSv2S_R0F_IRQ.INT_RPLY_SHORT_ADDR = 0x1F;
        mbus_remote_register_write(FLS_ADDR, 0x0F, FLSv2S_R0F_IRQ.as_int);

        // Set HALT_UNTIL_REG0
        set_halt_until_reg(7);
    
        // Start Flash Erase
        FLSv2S_R07_GO.GO = 0x1;
        mbus_remote_register_write(FLS_ADDR, 0x07, FLSv2S_R07_GO.as_int);

        // Check the payload
        if (*REG7 == ERASE_PASS) { pass (0x8, *REG7); }
        else { fail (0x8, *REG7); }

        // Set HALT_UNTIL_MBUS_TX
        set_halt_until_mbus_tx();
    }
}

void cycle5 (void) {
    if (do_cycle5 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();
    
        // Copy SRAM data into Flash SRAM (bulk)
        // Source Addr: 0x00000000 ~ 0x0000000F (16 words)
        // Target Addr: 0x00000000~
        mbus_copy_mem_from_local_to_remote_bulk (FLS_ADDR, 0x00000000, 0x00000000, 15);
    
        // Put the Flash SRAM data on the bus (to NODE_A)
        set_halt_until_mbus_fwd();
        mbus_copy_mem_from_remote_to_any_bulk (FLS_ADDR, 0x00000000, NODE_A_ADDR, 0x00000000, 15);

        // Put the Flash SRAM data (very long) on the bus (to NODE_B)
        set_halt_until_mbus_tx();
        mbus_copy_mem_from_remote_to_any_bulk (FLS_ADDR, 0x00000000, NODE_B_ADDR, 0x00000000, 127);

        // Halt CPU
        set_halt_until_mbus_fwd();  // CPU will resume when an MBus FWD operation is done.
        halt_cpu();                 // Halt CPU!

        // Read FLS's REG#0x23 ~ REG#0x27 (5 Registers) 
        set_halt_until_mbus_rx();
        mbus_copy_registers_from_remote_to_local (FLS_ADDR, 0x23, 0x00, 4);

        // Put those register values on the bus (to an fake address)
        set_halt_until_mbus_tx();
        mbus_copy_registers_from_local_to_remote (0xD, 0x15, 0x00, 4);
    }
}

void cycle6 (void) {
    if (do_cycle6 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();

        // Copy SRAM data into Flash SRAM (stream)
        // From 0x00000000, a quarter of SRAM
        mbus_copy_mem_from_local_to_remote_stream (0, FLS_ADDR, 0x00000000, 511);
    
        // Put the Flash SRAM data on the bus (to NODE_A)
        set_halt_until_mbus_fwd();
        mbus_copy_mem_from_remote_to_any_stream (0, FLS_ADDR, 0x00000000, NODE_A_ADDR, 511);

        // Put the Flash SRAM data (very long) on the bus (to NODE_B). I will use halt_cpu() here.
        set_halt_until_mbus_tx();
        mbus_copy_mem_from_remote_to_any_stream (1, FLS_ADDR, 0x00000000, NODE_B_ADDR, 511);

        // Halt CPU
        set_halt_until_mbus_fwd();  // CPU will resume when an MBus FWD operation is done.
        halt_cpu();                 // Halt CPU!

        // Set Halt
        set_halt_until_mbus_tx();
    }
}

void cycle7 (void) {
    if (do_cycle7 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();

        // Enable TIMER16 IRQ
        clear_all_pend_irq();
        *NVIC_ISER =  (0 /*GPIO*/ << 16)    | (0 /*SPI*/ << 15)     | (0 /*GOCEP*/ << 14)  | (0 /*MBUS_FWD*/ << 13) 
                    | (0 /*MBUS_TX*/ << 12) | (0 /*MBUS_RX*/ << 11) | (0 /*MEM_WR*/ << 10) | (0 /*REG7*/ << 9) 
                    | (0 /*REG6*/ << 8)     | (0 /*REG5*/ << 7)     | (0 /*REG4*/ << 6)    | (0 /*REG3*/ << 5) 
                    | (0 /*REG2*/ << 4)     | (0 /*REG1*/ << 3)     | (0 /*REG0*/ << 2)     
                    | (1 /*TIMER16*/ << 1)  | (0 /*TIMER32*/ << 0);

        // Timer16
        config_timer16(/*cmp0*/ 0x1000, /*cmp1*/0xF000, /*irq_en*/0x3, /*cnt*/0x0, /*status*/0x0);  // Start Timer16. Trigger when it reaches 0x1000 or 0xF000.

        // Wait for Interrupt
        WFI();

        // 1 IRQ: TIMER16
        if (irq_history == (1 << 1)) { 
               pass (0x9, irq_history); irq_history = 0;}
        else { fail (0x9, irq_history); }

        // Wait for Interrupt
        WFI();

        // 1 IRQ: TIMER16
        if (irq_history == (1 << 1)) {
               pass (0xA, irq_history); irq_history = 0; disable_all_irq(); }
        else { fail (0xA, irq_history); disable_all_irq(); }

        // Reset Timer16
        *TIMER16_GO = 0x0;
        *TIMER16_CNT = 0x0;
    }
}

void cycle8 (void) {
    if (do_cycle8 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();

        // Enable TIMER32 IRQ
        clear_all_pend_irq();
        *NVIC_ISER =  (0 /*GPIO*/ << 16)    | (0 /*SPI*/ << 15)     | (0 /*GOCEP*/ << 14)  | (0 /*MBUS_FWD*/ << 13) 
                    | (0 /*MBUS_TX*/ << 12) | (0 /*MBUS_RX*/ << 11) | (0 /*MEM_WR*/ << 10) | (0 /*REG7*/ << 9) 
                    | (0 /*REG6*/ << 8)     | (0 /*REG5*/ << 7)     | (0 /*REG4*/ << 6)    | (0 /*REG3*/ << 5) 
                    | (0 /*REG2*/ << 4)     | (0 /*REG1*/ << 3)     | (0 /*REG0*/ << 2)     
                    | (0 /*TIMER16*/ << 1)  | (1 /*TIMER32*/ << 0);

        // Timer16 with ROI (Reset-On-Interrupt)
        config_timer32(/*cmp*/ 0x2000, /*roi*/ 0x1, /*cnt*/ 0x0, /*status*/ 0x0); // Start Timer32. Trigger when it reaches 0x2000. Reset on Interrupt.

        // Wait for Interrupt
        WFI();

        // 1 IRQ: TIMER16
        if (irq_history == (1 << 0)) { 
               pass (0xB, irq_history); irq_history = 0;}
        else { fail (0xB, irq_history); }

        // Reset Timer16
        *TIMER32_GO = 0x0;
        *TIMER32_CNT = 0x0;
    }
}

void cycle9 (void) {
    if (do_cycle9 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();

        // Watch-Dog Timer (You should see TIMERWD triggered)
        config_timerwd(100);
        delay(200);

        // Reset Watch-Dog Timer
        config_timerwd(0x001E8480); // Default value. Approx. ~20 sec @ 100kHz
    }
}

void cycle10 (void) {
    if (do_cycle10 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();

        // Enable I/O Pads
        enable_io_pad();

        *SPI_SSPCR0 = 0x0287; // Motorola Mode
        *SPI_SSPCR1 = 0x2;
        *SPI_SSPCPSR = 0x02; // Clock Prescale Register
        //*SPI_SSPDMACR = 0x3; 
        *SPI_SSPDR = 0x1234;

        delay(1000);

        // Disable I/O Pads
        disable_io_pad();
    }
}

void cycle11 (void) {
    if (do_cycle11 == 1) {
        // Set Halt
        set_halt_until_mbus_tx();

        gpio_init(0xFFFF); // All Output
        gpio_write_data(0xF0F0); // 1111 0000 1111 0000
        gpio_kill_bit(15);       // 0111 0000 1111 0000
        gpio_set_bit(1);         // 0111 0000 1111 0010
        gpio_set_2bits(11, 8);   // 0111 1001 1111 0010
        gpio_kill_bit(12);       // 0110 1001 1111 0010

        if (gpio_get_data() == 0xF2) {pass (0xC, gpio_get_data());}
        else {fail (0xC, gpio_get_data());}

        gpio_close();
    }
}


//********************************************************************
// MAIN function starts here             
//********************************************************************

int main() {
    //Initialize Interrupts
    disable_all_irq();

    if (*REG_CHIP_ID != 0x1234) while(1);
  
    // Initialization Sequence
    if (enumerated != 0xDEADBEEF) { 
        initialization();
    }

    // Testing Sequence
    if      (cyc_num == 0)  cycle0();
    else if (cyc_num == 1)  cycle1();
    else if (cyc_num == 2)  cycle2();
    else if (cyc_num == 3)  cycle3();
    else if (cyc_num == 4)  cycle4();
    else if (cyc_num == 5)  cycle5();
    else if (cyc_num == 6)  cycle6();
    else if (cyc_num == 7)  cycle7();
    else if (cyc_num == 8)  cycle8();
    else if (cyc_num == 9)  cycle9();
    else if (cyc_num == 10) cycle10();
    else if (cyc_num == 11) cycle11();
    else cyc_num = 999;

    // Sleep/Wakeup OR Terminate operation
    if (cyc_num == 999) *REG_CHIP_ID = 0xFFFF; // This will stop the verilog sim.
    else {
        cyc_num++;
        //set_wakeup_timer(5, 1, 1);
        *WUPT_RESET = 0x1;
        mbus_sleep_all();
    }

    while(1){  //Never Quit (should not come here.)
        arb_debug_reg (0xDEADBEEF);
        asm("nop;"); 
    }

    return 1;
}
