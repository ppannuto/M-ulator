//************************************************************
// Desciption: MRRv11A Register File Header File
//      Generated by genRF (Version 1.42) 07/29/2020 14:35:47
//************************************************************

#ifndef MRRV11A_RF_H
#define MRRV11A_RF_H

// Register 0x00
typedef union mrrv11a_r00{
  struct{
    unsigned TRX_CL_EN		: 1;
    unsigned TRX_CL_CTRL		: 6;
    unsigned TRX_CAP_ANTP_TUNE_COARSE		: 10;
    unsigned NOT_DEFINED_0_17		: 1;
    unsigned NOT_DEFINED_0_18		: 1;
    unsigned NOT_DEFINED_0_19		: 1;
    unsigned NOT_DEFINED_0_20		: 1;
    unsigned TRX_CNT_EN		: 1;
    unsigned TRX_CNT_RESET_B		: 1;
  };
  uint32_t as_int;
} mrrv11a_r00_t;
#define MRRv11A_R00_DEFAULT {{0x0, 0x08, 0x000, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0}}
#define MRRv11A_R00_DEFAULT_AS_INT 0x000010
_Static_assert(sizeof(mrrv11a_r00_t) == 4, "Punned Structure Size");

// Register 0x01
typedef union mrrv11a_r01{
  struct{
    unsigned TRX_CAP_ANTN_TUNE_COARSE		: 10;
    unsigned TRX_CAP_ANTN_TUNE_FINE		: 6;
    unsigned TRX_CAP_ANTP_TUNE_FINE		: 6;
  };
  uint32_t as_int;
} mrrv11a_r01_t;
#define MRRv11A_R01_DEFAULT {{0x000, 0x00, 0x00}}
#define MRRv11A_R01_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r01_t) == 4, "Punned Structure Size");

// Register 0x02
typedef union mrrv11a_r02{
  struct{
    unsigned TX_BIAS_TUNE		: 13;
    unsigned TX_EN_OW		: 1;
    unsigned TX_PULSE_FINE		: 1;
    unsigned TX_PULSE_FINE_TUNE		: 4;
  };
  uint32_t as_int;
} mrrv11a_r02_t;
#define MRRv11A_R02_DEFAULT {{0x1FFF, 0x1, 0x0, 0x7}}
#define MRRv11A_R02_DEFAULT_AS_INT 0x03BFFF
_Static_assert(sizeof(mrrv11a_r02_t) == 4, "Punned Structure Size");

// Register 0x03
typedef union mrrv11a_r03{
  struct{
    unsigned RX_BIAS_TUNE		: 13;
    unsigned RX_EN_OW		: 1;
    unsigned TRX_MODE_EN		: 1;
    unsigned RX_SAMPLE_CAP		: 3;
    unsigned TRX_DCP_S_OW2		: 1;
    unsigned TRX_DCP_S_OW1		: 1;
    unsigned TRX_DCP_P_OW2		: 1;
    unsigned TRX_DCP_P_OW1		: 1;
    unsigned TRX_ISOLATEN		: 1;
    unsigned RX_AMP_OW_EN		: 1;
  };
  uint32_t as_int;
} mrrv11a_r03_t;
#define MRRv11A_R03_DEFAULT {{0x0801, 0x0, 0x0, 0x0, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0}}
#define MRRv11A_R03_DEFAULT_AS_INT 0x040801
_Static_assert(sizeof(mrrv11a_r03_t) == 4, "Punned Structure Size");

// Register 0x04
typedef union mrrv11a_r04{
  struct{
    unsigned RO_EN_RO_V1P2		: 1;
    unsigned RO_EN_RO_V1P2_PRE		: 1;
    unsigned RO_EN_RO_LDO		: 1;
    unsigned RO_RESET		: 1;
    unsigned RO_ISOLATE_CLK		: 1;
    unsigned RO_EN_CLK		: 1;
    unsigned RO_SEL_EXT_CLK		: 1;
    unsigned RO_SEL_DIV		: 2;
    unsigned RO_EN_MONITOR		: 1;
    unsigned LDO_EN_LDO		: 1;
    unsigned LDO_EN_IREF		: 1;
    unsigned LDO_EN_VREF		: 1;
    unsigned LDO_SEL_VOUT		: 3;
    unsigned LDO_VREF_I_AMP		: 4;
  };
  uint32_t as_int;
} mrrv11a_r04_t;
#define MRRv11A_R04_DEFAULT {{0x0, 0x0, 0x0, 0x1, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x4, 0x0}}
#define MRRv11A_R04_DEFAULT_AS_INT 0x008018
_Static_assert(sizeof(mrrv11a_r04_t) == 4, "Punned Structure Size");

// Register 0x05
typedef union mrrv11a_r05{
  struct{
    unsigned RO_V_MIRRb		: 4;
    unsigned RO_I_MIRROR2		: 2;
    unsigned RO_I_MIRROR		: 2;
    unsigned RO_I_CMP		: 4;
    unsigned RO_I_BUF2		: 4;
    unsigned RO_I_BUF		: 4;
    unsigned RO_R_REF		: 4;
  };
  uint32_t as_int;
} mrrv11a_r05_t;
#define MRRv11A_R05_DEFAULT {{0xD, 0x1, 0x1, 0x1, 0x0, 0x0, 0x8}}
#define MRRv11A_R05_DEFAULT_AS_INT 0x80015D
_Static_assert(sizeof(mrrv11a_r05_t) == 4, "Punned Structure Size");

// Register 0x06
typedef union mrrv11a_r06{
  struct{
    unsigned RO_PDIFF		: 15;
  };
  uint32_t as_int;
} mrrv11a_r06_t;
#define MRRv11A_R06_DEFAULT {{0x1000}}
#define MRRv11A_R06_DEFAULT_AS_INT 0x001000
_Static_assert(sizeof(mrrv11a_r06_t) == 4, "Punned Structure Size");

// Register 0x07
typedef union mrrv11a_r07{
  struct{
    unsigned RO_MOM		: 7;
    unsigned RO_MIM		: 7;
  };
  uint32_t as_int;
} mrrv11a_r07_t;
#define MRRv11A_R07_DEFAULT {{0x10, 0x10}}
#define MRRv11A_R07_DEFAULT_AS_INT 0x000810
_Static_assert(sizeof(mrrv11a_r07_t) == 4, "Punned Structure Size");

// Register 0x08
typedef union mrrv11a_r08{
  struct{
    unsigned RO_POLY		: 24;
  };
  uint32_t as_int;
} mrrv11a_r08_t;
#define MRRv11A_R08_DEFAULT {{0x400000}}
#define MRRv11A_R08_DEFAULT_AS_INT 0x400000
_Static_assert(sizeof(mrrv11a_r08_t) == 4, "Punned Structure Size");

// Register 0x09
typedef union mrrv11a_r09{
  struct{
    unsigned MEM_TX_WRITE_ONLY		: 1;
  };
  uint32_t as_int;
} mrrv11a_r09_t;
#define MRRv11A_R09_DEFAULT {{0x0}}
#define MRRv11A_R09_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r09_t) == 4, "Punned Structure Size");

// Register 0x0A
// -- EMPTY --

// Register 0x0B
// -- EMPTY --

// Register 0x0C
// -- EMPTY --

// Register 0x0D
// -- EMPTY --

// Register 0x0E
// -- EMPTY --

// Register 0x0F
// -- EMPTY --

// Register 0x10
typedef union mrrv11a_r10{
  struct{
    unsigned FSM_TX_TP_LEN		: 8;
    unsigned FSM_TX_PASSCODE		: 16;
  };
  uint32_t as_int;
} mrrv11a_r10_t;
#define MRRv11A_R10_DEFAULT {{0x50, 0x7AC8}}
#define MRRv11A_R10_DEFAULT_AS_INT 0x7AC850
_Static_assert(sizeof(mrrv11a_r10_t) == 4, "Punned Structure Size");

// Register 0x11
typedef union mrrv11a_r11{
  struct{
    unsigned FSM_SLEEP		: 1;
    unsigned FSM_RESET_B		: 1;
    unsigned FSM_EN		: 1;
    unsigned FSM_CONT_PULSE_MODE		: 1;
    unsigned NOT_DEFINED_17_4		: 1;
    unsigned NOT_DEFINED_17_5		: 1;
    unsigned NOT_DEFINED_17_6		: 1;
    unsigned NOT_DEFINED_17_7		: 1;
    unsigned FSM_TX_DATA_BITS		: 9;
  };
  uint32_t as_int;
} mrrv11a_r11_t;
#define MRRv11A_R11_DEFAULT {{0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0C0}}
#define MRRv11A_R11_DEFAULT_AS_INT 0x00C001
_Static_assert(sizeof(mrrv11a_r11_t) == 4, "Punned Structure Size");

// Register 0x12
typedef union mrrv11a_r12{
  struct{
    unsigned FSM_TX_PW_LEN		: 10;
    unsigned FSM_TX_PS_LEN		: 10;
    unsigned FSM_TX_PR_LEN		: 3;
  };
  uint32_t as_int;
} mrrv11a_r12_t;
#define MRRv11A_R12_DEFAULT {{0x018, 0x031, 0x0}}
#define MRRv11A_R12_DEFAULT_AS_INT 0x00C418
_Static_assert(sizeof(mrrv11a_r12_t) == 4, "Punned Structure Size");

// Register 0x13
typedef union mrrv11a_r13{
  struct{
    unsigned FSM_TX_C_LEN		: 15;
  };
  uint32_t as_int;
} mrrv11a_r13_t;
#define MRRv11A_R13_DEFAULT {{0x0064}}
#define MRRv11A_R13_DEFAULT_AS_INT 0x000064
_Static_assert(sizeof(mrrv11a_r13_t) == 4, "Punned Structure Size");

// Register 0x14
typedef union mrrv11a_r14{
  struct{
    unsigned NOT_DEFINED_20_0		: 1;
    unsigned NOT_DEFINED_20_1		: 1;
    unsigned FSM_GUARD_LEN		: 14;
    unsigned FSM_TX_POWERON_LEN		: 3;
    unsigned FSM_RX_POWERON_LEN		: 2;
    unsigned FSM_RX_SAMPLE_LEN		: 3;
  };
  uint32_t as_int;
} mrrv11a_r14_t;
#define MRRv11A_R14_DEFAULT {{0x0, 0x0, 0x0000, 0x0, 0x0, 0x0}}
#define MRRv11A_R14_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r14_t) == 4, "Punned Structure Size");

// Register 0x15
typedef union mrrv11a_r15{
  struct{
    unsigned FSM_RX_DATA_BITS		: 8;
  };
  uint32_t as_int;
} mrrv11a_r15_t;
#define MRRv11A_R15_DEFAULT {{0x00}}
#define MRRv11A_R15_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r15_t) == 4, "Punned Structure Size");

// Register 0x16
typedef union mrrv11a_r16{
  struct{
    unsigned DIG_MONITOR_SEL1		: 4;
    unsigned DIG_MONITOR_SEL2		: 4;
    unsigned DIG_MONITOR_SEL3		: 4;
    unsigned DIG_MONITOR_ENABLE		: 1;
  };
  uint32_t as_int;
} mrrv11a_r16_t;
#define MRRv11A_R16_DEFAULT {{0x0, 0x0, 0x0, 0x0}}
#define MRRv11A_R16_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r16_t) == 4, "Punned Structure Size");

// Register 0x17
// -- READ-ONLY --

// Register 0x18
// -- EMPTY --

// Register 0x19
// -- EMPTY --

// Register 0x1A
// -- EMPTY --

// Register 0x1B
// -- EMPTY --

// Register 0x1C
// -- EMPTY --

// Register 0x1D
// -- EMPTY --

// Register 0x1E
typedef union mrrv11a_r1E{
  struct{
    unsigned FSM_IRQ_REPLY_PACKET		: 24;
  };
  uint32_t as_int;
} mrrv11a_r1E_t;
#define MRRv11A_R1E_DEFAULT {{0x001002}}
#define MRRv11A_R1E_DEFAULT_AS_INT 0x001002
_Static_assert(sizeof(mrrv11a_r1E_t) == 4, "Punned Structure Size");

// Register 0x1F
typedef union mrrv11a_r1F{
  struct{
    unsigned LC_CLK_RING		: 2;
    unsigned LC_CLK_DIV		: 2;
    unsigned LC_CLK_LOAD		: 1;
  };
  uint32_t as_int;
} mrrv11a_r1F_t;
#define MRRv11A_R1F_DEFAULT {{0x3, 0x3, 0x0}}
#define MRRv11A_R1F_DEFAULT_AS_INT 0x00000F
_Static_assert(sizeof(mrrv11a_r1F_t) == 4, "Punned Structure Size");

// Register 0x20
// -- READ-ONLY --

// Register 0x21
typedef union mrrv11a_r21{
  struct{
    unsigned TRX_EN_DIV		: 1;
    unsigned TRX_ENb_COMP		: 1;
    unsigned TRX_ISOLn_COMP		: 1;
    unsigned TRX_RESETn_RC_CNT		: 1;
    unsigned TRX_SEL_VCLAMP		: 6;
    unsigned TRX_ENb_CONT_RC		: 1;
    unsigned TRX_EN_SLP_VCLAMP		: 1;
    unsigned TRX_SEL_VCLAMP_SLP		: 2;
    unsigned TRX_IRQ_EN		: 1;
  };
  uint32_t as_int;
} mrrv11a_r21_t;
#define MRRv11A_R21_DEFAULT {{0x0, 0x1, 0x0, 0x0, 0x11, 0x1, 0x0, 0x0, 0x0}}
#define MRRv11A_R21_DEFAULT_AS_INT 0x000512
_Static_assert(sizeof(mrrv11a_r21_t) == 4, "Punned Structure Size");

// Register 0x22
// -- READ-ONLY --

// Register 0x23
typedef union mrrv11a_r23{
  struct{
    unsigned TRX_IRQ_REPLY_PACKET		: 24;
  };
  uint32_t as_int;
} mrrv11a_r23_t;
#define MRRv11A_R23_DEFAULT {{0x001003}}
#define MRRv11A_R23_DEFAULT_AS_INT 0x001003
_Static_assert(sizeof(mrrv11a_r23_t) == 4, "Punned Structure Size");

// Register 0x24
// -- EMPTY --

// Register 0x25
// -- EMPTY --

// Register 0x26
// -- EMPTY --

// Register 0x27
// -- EMPTY --

// Register 0x28
// -- EMPTY --

// Register 0x29
// -- EMPTY --

// Register 0x2A
typedef union mrrv11a_r2A{
  struct{
    unsigned SEL_V1P2_BIAS		: 1;
    unsigned SEL_VLDO_BIAS		: 1;
  };
  uint32_t as_int;
} mrrv11a_r2A_t;
#define MRRv11A_R2A_DEFAULT {{0x1, 0x0}}
#define MRRv11A_R2A_DEFAULT_AS_INT 0x000001
_Static_assert(sizeof(mrrv11a_r2A_t) == 4, "Punned Structure Size");

// Register 0x2B
typedef union mrrv11a_r2B{
  struct{
    unsigned RF_GOC_OUT_ENABLE		: 1;
    unsigned RF_GOC_SEL_OSC_STAGE		: 1;
    unsigned RF_GOC_SEL_DIV		: 2;
    unsigned EN_DECAP_CHG		: 1;
  };
  uint32_t as_int;
} mrrv11a_r2B_t;
#define MRRv11A_R2B_DEFAULT {{0x1, 0x0, 0x0, 0x0}}
#define MRRv11A_R2B_DEFAULT_AS_INT 0x000001
_Static_assert(sizeof(mrrv11a_r2B_t) == 4, "Punned Structure Size");

// Register 0x2C
// -- EMPTY --

// Register 0x2D
// -- EMPTY --

// Register 0x2E
// -- EMPTY --

// Register 0x2F
// -- EMPTY --

// Register 0x30
// -- EMPTY --

// Register 0x31
// -- EMPTY --

// Register 0x32
// -- EMPTY --

// Register 0x33
// -- EMPTY --

// Register 0x34
// -- EMPTY --

// Register 0x35
// -- EMPTY --

// Register 0x36
// -- EMPTY --

// Register 0x37
// -- EMPTY --

// Register 0x38
// -- EMPTY --

// Register 0x39
// -- EMPTY --

// Register 0x3A
// -- EMPTY --

// Register 0x3B
// -- EMPTY --

// Register 0x3C
// -- EMPTY --

// Register 0x3D
typedef union mrrv11a_r3D{
  struct{
    unsigned NOT_DEFINED_61_0		: 1;
    unsigned NOT_DEFINED_61_1		: 1;
    unsigned NOT_DEFINED_61_2		: 1;
    unsigned NOT_DEFINED_61_3		: 1;
    unsigned NOT_DEFINED_61_4		: 1;
    unsigned NOT_DEFINED_61_5		: 1;
    unsigned NOT_DEFINED_61_6		: 1;
    unsigned NOT_DEFINED_61_7		: 1;
    unsigned NOT_DEFINED_61_8		: 1;
    unsigned NOT_DEFINED_61_9		: 1;
    unsigned NOT_DEFINED_61_10		: 1;
    unsigned NOT_DEFINED_61_11		: 1;
    unsigned NOT_DEFINED_61_12		: 1;
    unsigned NOT_DEFINED_61_13		: 1;
    unsigned NOT_DEFINED_61_14		: 1;
    unsigned NOT_DEFINED_61_15		: 1;
    unsigned STR_WR_CH0_ALT_ADDR		: 8;
  };
  uint32_t as_int;
} mrrv11a_r3D_t;
#define MRRv11A_R3D_DEFAULT {{0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xF0}}
#define MRRv11A_R3D_DEFAULT_AS_INT 0xF00000
_Static_assert(sizeof(mrrv11a_r3D_t) == 4, "Punned Structure Size");

// Register 0x3E
typedef union mrrv11a_r3E{
  struct{
    unsigned NOT_DEFINED_62_0		: 1;
    unsigned NOT_DEFINED_62_1		: 1;
    unsigned NOT_DEFINED_62_2		: 1;
    unsigned NOT_DEFINED_62_3		: 1;
    unsigned NOT_DEFINED_62_4		: 1;
    unsigned NOT_DEFINED_62_5		: 1;
    unsigned NOT_DEFINED_62_6		: 1;
    unsigned NOT_DEFINED_62_7		: 1;
    unsigned NOT_DEFINED_62_8		: 1;
    unsigned NOT_DEFINED_62_9		: 1;
    unsigned NOT_DEFINED_62_10		: 1;
    unsigned NOT_DEFINED_62_11		: 1;
    unsigned NOT_DEFINED_62_12		: 1;
    unsigned NOT_DEFINED_62_13		: 1;
    unsigned NOT_DEFINED_62_14		: 1;
    unsigned NOT_DEFINED_62_15		: 1;
    unsigned STR_WR_CH0_ALT_REG_WR		: 8;
  };
  uint32_t as_int;
} mrrv11a_r3E_t;
#define MRRv11A_R3E_DEFAULT {{0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x00}}
#define MRRv11A_R3E_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r3E_t) == 4, "Punned Structure Size");

// Register 0x3F
typedef union mrrv11a_r3F{
  struct{
    unsigned STR_WR_CH0_BUF_LEN		: 3;
    unsigned NOT_DEFINED_63_3		: 1;
    unsigned NOT_DEFINED_63_4		: 1;
    unsigned NOT_DEFINED_63_5		: 1;
    unsigned NOT_DEFINED_63_6		: 1;
    unsigned NOT_DEFINED_63_7		: 1;
    unsigned NOT_DEFINED_63_8		: 1;
    unsigned NOT_DEFINED_63_9		: 1;
    unsigned NOT_DEFINED_63_10		: 1;
    unsigned NOT_DEFINED_63_11		: 1;
    unsigned NOT_DEFINED_63_12		: 1;
    unsigned NOT_DEFINED_63_13		: 1;
    unsigned NOT_DEFINED_63_14		: 1;
    unsigned NOT_DEFINED_63_15		: 1;
    unsigned NOT_DEFINED_63_16		: 1;
    unsigned NOT_DEFINED_63_17		: 1;
    unsigned NOT_DEFINED_63_18		: 1;
    unsigned NOT_DEFINED_63_19		: 1;
    unsigned NOT_DEFINED_63_20		: 1;
    unsigned STR_WR_CH0_DBLB		: 1;
    unsigned STR_WR_CH0_WRP		: 1;
    unsigned STR_WR_CH0_EN		: 1;
  };
  uint32_t as_int;
} mrrv11a_r3F_t;
#define MRRv11A_R3F_DEFAULT {{0x7, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1}}
#define MRRv11A_R3F_DEFAULT_AS_INT 0x800007
_Static_assert(sizeof(mrrv11a_r3F_t) == 4, "Punned Structure Size");

// Register 0x40
typedef union mrrv11a_r40{
  struct{
    unsigned STR_WR_CH0_BUF_OFF		: 3;
  };
  uint32_t as_int;
} mrrv11a_r40_t;
#define MRRv11A_R40_DEFAULT {{0x0}}
#define MRRv11A_R40_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r40_t) == 4, "Punned Structure Size");

// Register 0x41
// -- EMPTY --

// Register 0x42
// -- EMPTY --

// Register 0x43
typedef union mrrv11a_r43{
  struct{
    unsigned NOT_DEFINED_67_0		: 1;
    unsigned NOT_DEFINED_67_1		: 1;
    unsigned NOT_DEFINED_67_2		: 1;
    unsigned NOT_DEFINED_67_3		: 1;
    unsigned NOT_DEFINED_67_4		: 1;
    unsigned NOT_DEFINED_67_5		: 1;
    unsigned NOT_DEFINED_67_6		: 1;
    unsigned NOT_DEFINED_67_7		: 1;
    unsigned NOT_DEFINED_67_8		: 1;
    unsigned NOT_DEFINED_67_9		: 1;
    unsigned NOT_DEFINED_67_10		: 1;
    unsigned NOT_DEFINED_67_11		: 1;
    unsigned NOT_DEFINED_67_12		: 1;
    unsigned NOT_DEFINED_67_13		: 1;
    unsigned NOT_DEFINED_67_14		: 1;
    unsigned NOT_DEFINED_67_15		: 1;
    unsigned NOT_DEFINED_67_16		: 1;
    unsigned NOT_DEFINED_67_17		: 1;
    unsigned NOT_DEFINED_67_18		: 1;
    unsigned NOT_DEFINED_67_19		: 1;
    unsigned NOT_DEFINED_67_20		: 1;
    unsigned NOT_DEFINED_67_21		: 1;
    unsigned NOT_DEFINED_67_22		: 1;
    unsigned BLK_WR_EN		: 1;
  };
  uint32_t as_int;
} mrrv11a_r43_t;
#define MRRv11A_R43_DEFAULT {{0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1}}
#define MRRv11A_R43_DEFAULT_AS_INT 0x800000
_Static_assert(sizeof(mrrv11a_r43_t) == 4, "Punned Structure Size");

// Register 0x44
// -- EMPTY --

// Register 0x45
// -- EMPTY --

// Register 0x46
// -- EMPTY --

// Register 0x47
// -- EMPTY --

// Register 0x48
// -- EMPTY --

// Register 0x49
// -- EMPTY --

// Register 0x4A
// -- EMPTY --

// Register 0x4B
// -- EMPTY --

// Register 0x4C
// -- EMPTY --

// Register 0x4D
// -- EMPTY --

// Register 0x4E
// -- EMPTY --

// Register 0x4F
// -- EMPTY --

// Register 0x50
typedef union mrrv11a_r50{
  struct{
    unsigned NOT_DEFINED_80_0		: 1;
    unsigned NOT_DEFINED_80_1		: 1;
    unsigned NOT_DEFINED_80_2		: 1;
    unsigned NOT_DEFINED_80_3		: 1;
    unsigned NOT_DEFINED_80_4		: 1;
    unsigned NOT_DEFINED_80_5		: 1;
    unsigned NOT_DEFINED_80_6		: 1;
    unsigned NOT_DEFINED_80_7		: 1;
    unsigned NOT_DEFINED_80_8		: 1;
    unsigned NOT_DEFINED_80_9		: 1;
    unsigned NOT_DEFINED_80_10		: 1;
    unsigned NOT_DEFINED_80_11		: 1;
    unsigned NOT_DEFINED_80_12		: 1;
    unsigned NOT_DEFINED_80_13		: 1;
    unsigned NOT_DEFINED_80_14		: 1;
    unsigned NOT_DEFINED_80_15		: 1;
    unsigned NOT_DEFINED_80_16		: 1;
    unsigned NOT_DEFINED_80_17		: 1;
    unsigned NOT_DEFINED_80_18		: 1;
    unsigned NOT_DEFINED_80_19		: 1;
    unsigned NOT_DEFINED_80_20		: 1;
    unsigned NOT_DEFINED_80_21		: 1;
    unsigned NOT_DEFINED_80_22		: 1;
    unsigned ACT_RST		: 1;
  };
  uint32_t as_int;
} mrrv11a_r50_t;
#define MRRv11A_R50_DEFAULT {{0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0}}
#define MRRv11A_R50_DEFAULT_AS_INT 0x000000
_Static_assert(sizeof(mrrv11a_r50_t) == 4, "Punned Structure Size");

#endif // MRRV11A_RF_H
