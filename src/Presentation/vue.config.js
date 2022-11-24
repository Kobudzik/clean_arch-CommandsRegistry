const CopyPlugin = require('copy-webpack-plugin');

module.exports = {
    outputDir: process.env.VUE_APP_OUTPUT,
    configureWebpack: {
        plugins: [
            new CopyPlugin({
                patterns: [
                    {
                        from: 'src/robots.txt',
                        to: 'robots.txt'
                    }
                ]
            }),
        ]
    },
    pluginOptions: {
        webpackBundleAnalyzer: {
            openAnalyzer: false
        }
    }
}
