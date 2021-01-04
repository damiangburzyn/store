module.exports = {

    devServer: {
        proxy: 'https://localhost:44309/',
    },
    //publicPath: process.env.NODE_ENV == 'development' ? 'http://localhost:8100' : '/',



    //lintOnSave: false,
    //runtimeCompiler: true,
    filenameHashing: false,
    //productionSourceMap: false,

    transpileDependencies: ['vuex-module-decorators '],



    // configureWebpack: {
    //    optimization: {
    //        splitChunks: false
    //    }
    //}


    //configureWebpack: {
    //    plugins: [
    //        new Css()
    //    ]
    //},

};