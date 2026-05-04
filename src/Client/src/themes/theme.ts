import { definePreset } from '@primeuix/themes';
import Aura from '@primeuix/themes/aura';

export const DaisyLightPreset = definePreset(Aura, {
    semantic: {
        primary: {
            50: 'oklch(80% 0.105 251.813)',
            100: 'oklch(80% 0.105 251.813)',
            200: 'oklch(80% 0.105 251.813)',
            300: 'oklch(80% 0.105 251.813)',
            400: 'oklch(80% 0.105 251.813)',
            500: 'oklch(80% 0.105 251.813)',
            600: 'oklch(80% 0.105 251.813)',
            700: 'oklch(80% 0.105 251.813)',
            800: 'oklch(80% 0.105 251.813)',
            900: 'oklch(80% 0.105 251.813)'
        },

        colorScheme: {
            light: {
                surface: {
                    0: 'oklch(98% 0.003 247.858)', // base-100
                    50: 'oklch(98% 0.002 247.839)', // base-200
                    100: 'oklch(98% 0 0)',           // base-300
                    200: 'oklch(98% 0 0)'
                },

                text: {
                    color: 'oklch(20% 0.042 265.755)',   // base-content
                    secondaryColor: 'oklch(55% 0 0)'     // neutral-content
                },

                highlight: {
                    background: 'oklch(80% 0.105 251.813)', // primary
                    color: 'oklch(97% 0.014 254.604)'       // primary-content
                }
            }
        }
    }

});

export const DaisyDarkPreset = definePreset(Aura, {
  semantic: {
    primary: {
      50:  'oklch(35% 0.144 278.697)',
      100: 'oklch(35% 0.144 278.697)',
      200: 'oklch(35% 0.144 278.697)',
      300: 'oklch(35% 0.144 278.697)',
      400: 'oklch(35% 0.144 278.697)',
      500: 'oklch(35% 0.144 278.697)',
      600: 'oklch(35% 0.144 278.697)',
      700: 'oklch(35% 0.144 278.697)',
      800: 'oklch(35% 0.144 278.697)',
      900: 'oklch(35% 0.144 278.697)'
    },

    colorScheme: {
      dark: {
        surface: {
          0:   'oklch(14% 0.005 285.823)', // base-100
          50:  'oklch(27% 0.006 286.033)', // base-200
          100: 'oklch(44% 0.017 285.786)', // base-300
          200: 'oklch(44% 0.017 285.786)'
        },

        text: {
          color: 'oklch(97.807% 0.029 256.847)', // base-content
          secondaryColor: 'oklch(92% 0.004 286.32)' // neutral-content
        },

        highlight: {
          background: 'oklch(35% 0.144 278.697)', // primary
          color: 'oklch(96% 0.018 272.314)'       // primary-content
        }
      }
    }
  },
});
