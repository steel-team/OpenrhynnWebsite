// @ts-check
import withNuxt from "./.nuxt/eslint.config.mjs";

export default withNuxt(
  // Your custom configs here
  {
    files: ["**/*.ts", "**/*.tsx", "**/*.vue", "**/*.mjs"],
    rules: {
      quotes: ["error", "double", { avoidEscape: true }],
      semi: ["error", "always"],
      "no-empty": "off",
      "no-constant-condition": "off",
      "no-control-regex": "off",
      "@stylistic/operator-linebreak": "off",
      "@stylistic/quote-props": "off",
      "vue/max-attributes-per-line": "off",
      "vue/no-v-html": "off",
      "vue/html-self-closing": "off",
      "@stylistic/member-delimiter-style": "off",
      "@typescript-eslint/no-unused-vars": "off",
      "@stylistic/arrow-parens": "off",
      "@typescript-eslint/no-explicit-any": "off",
      "@stylistic/no-tabs": "off",
    },
  },
);
