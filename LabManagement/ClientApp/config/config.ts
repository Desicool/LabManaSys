// https://umijs.org/config/
import { defineConfig, utils } from 'umi';
import defaultSettings from './defaultSettings';
import proxy from './proxy';
import webpackPlugin from './plugin.config';
const { winPath } = utils; // preview.pro.ant.design only do not use in your production ;
// preview.pro.ant.design 专用环境变量，请不要在你的项目中使用它。

const { ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION, REACT_APP_ENV, GA_KEY } = process.env;
export default defineConfig({
  hash: true,
  antd: {},
  analytics: GA_KEY
    ? {
      ga: GA_KEY,
    }
    : false,
  dva: {
    hmr: true,
  },
  locale: {
    // default zh-CN
    default: 'zh-CN',
    // default true, when it is true, will use `navigator.language` overwrite default
    antd: true,
    baseNavigator: true,
  },
  dynamicImport: {
    loading: '@/components/PageLoading/index',
  },
  targets: {
    ie: 11,
  },
  // umi routes: https://umijs.org/docs/routing
  routes: [
    {
      path: '/user',
      component: '../layouts/UserLayout',
      routes: [
        {
          name: 'login',
          path: '/user/login',
          component: './user/login',
        },
      ],
    },
    {
      path: '/',
      component: '../layouts/SecurityLayout',
      routes: [
        {
          path: '/',
          component: '../layouts/BasicLayout',
          authority: ['Administrator', 'Student','LabTeacher','FinanceTeacher','Security',],
          routes: [
            {
              path: '/',
              redirect: '/my/process',
            },
            {
              path: '/admin',
              name: 'admin',
              icon: 'crown',
              component: './Admin',
              authority: ['Administrator'],
              routes: [
                {
                  path: '/admin/sub-page',
                  name: 'sub-page',
                  icon: 'smile',
                  component: './Welcome',
                  authority: ['Administrator'],
                },
              ],
            },
            {
              name: 'entity',
              path: '/query',
              routes: [
                {
                  name: 'labchemicals',
                  path: '/query/chemical',
                  component: './Query/ChemicalList',
                },
                {
                  name: 'mychemicals',
                  path: '/query/mychemical',
                  component: './Query/ChemicalsInUsing'
                },
              ],
            },
            {
              name: 'myworkflow',
              path: '/my/workflow',
              component: './My/WorkFlowList',
            },
            {
              name: 'workflowdetail',
              path: '/my/workflow/:workflowid',
              hideInMenu: true,
              component: './My/WorkFlowDetail',
            },
            {
              name: 'process',
              path: '/my/process',
              component: './My/FormToProcess',
            },
            {
              name: 'post',
              path: '/post',
              routes: [
                {
                  name: 'declear',
                  path: '/post/declear',
                  component: './Post/DeclarationForm',
                },
                {
                  name: 'financial',
                  path: '/post/financial',
                  component: './Post/FinancialForm',
                },
                {
                  name: 'claim',
                  path: '/post/claim',
                  component: './Post/ClaimForm',
                },
              ],
            },
            {
              name: 'processdeclear',
              path: '/process/declear/:formid',
              component: './Process/DeclearCommit',
              //hideInMenu: true,
              authority: ['LabTeacher','Adminstrator'],
            },
            {
              name: 'processfinancial',
              path: '/process/financial/:formid',
              component: './Process/FinancialCommit',
              //hideInMenu: true,
              authority: ['LabTeacher','Adminstrator'],
            },
            {
              name: 'processclaim',
              path: '/process/claim/:formid',
              component: './Process/ClaimCommit',
              //hideInMenu: true,
              authority: ['LabTeacher','Adminstrator'],
            },
            {
              name: 'claimdetail',
              path: '/my/process/claim/:formid',
              component: './Query/ClaimFormChemicals',
              hideInMenu: true,
            },
            {
              component: './404',
            },
          ],
        },
        {
          component: './404',
        },
      ],
    },
    {
      component: './404',
    },
  ],
  // Theme for antd: https://ant.design/docs/react/customize-theme-cn
  theme: {
    // ...darkTheme,
    'primary-color': defaultSettings.primaryColor,
  },
  define: {
    REACT_APP_ENV: REACT_APP_ENV || false,
    ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION:
      ANT_DESIGN_PRO_ONLY_DO_NOT_USE_IN_YOUR_PRODUCTION || '', // preview.pro.ant.design only do not use in your production ; preview.pro.ant.design 专用环境变量，请不要在你的项目中使用它。
  },
  ignoreMomentLocale: true,
  lessLoader: {
    javascriptEnabled: true,
  },
  cssLoader: {
    modules: {
      getLocalIdent: (
        context: {
          resourcePath: string;
        },
        _: string,
        localName: string
      ) => {
        if (
          context.resourcePath.includes('node_modules') ||
          context.resourcePath.includes('ant.design.pro.less') ||
          context.resourcePath.includes('global.less')
        ) {
          return localName;
        }

        const match = context.resourcePath.match(/src(.*)/);

        if (match && match[1]) {
          const antdProPath = match[1].replace('.less', '');
          const arr = winPath(antdProPath)
            .split('/')
            .map((a: string) => a.replace(/([A-Z])/g, '-$1'))
            .map((a: string) => a.toLowerCase());
          return `antd-pro${arr.join('-')}-${localName}`.replace(/--/g, '-');
        }

        return localName;
      },
    },
  },
  manifest: {
    basePath: '/',
  },
  proxy: proxy[REACT_APP_ENV || 'dev'],
  chainWebpack: webpackPlugin,
});
