import { Effect, Reducer } from 'umi';

import { IFinancialForm, IDeclarationForm, IWorkFlow, IChemical } from '@/models/entity';
import { getFinancialForms, getDeclarationForms, getWorkFlowById } from './service';
import moment from 'moment';

export interface WorkFlowDetailStateType {
    financialForms: IFinancialForm[];
    declarationForm?: IDeclarationForm;
    currentWorkFlow?: IWorkFlow;
}

export interface WorkFlowDetailModelType {
    namespace: string;
    state: WorkFlowDetailStateType;
    effects: {
        fetchWorkFlow: Effect;
    };
    reducers: {
        fetchWorkFlowSuccess: Reducer<WorkFlowDetailStateType>;
    };
}
const initState: WorkFlowDetailStateType = {
    financialForms: []
}
const Model: WorkFlowDetailModelType = {
    namespace: 'workFlowDetail',

    state: initState,

    effects: {
        *fetchWorkFlow({ payload }, { call, put }) {
            const workflow = yield call(getWorkFlowById, payload);
            const declaration : IDeclarationForm = yield call(getDeclarationForms, payload);
            const financial = yield call(getFinancialForms, payload);
            yield put({
                type: 'fetchWorkFlowSuccess',
                payload: {
                    currentWorkFlow: workflow,
                    declarationForm: {
                        ...declaration,
                        stime: moment(declaration.stime).format('YYYY-MM-DD HH:mm:ss')
                    },
                    financialForms: Array.isArray(financial) ? financial : [],
                }
            })
        }
    },

    reducers: {
        fetchWorkFlowSuccess(state = initState, action) {
            return {
                ...state,
                ...action.payload
            }
        }
    },
};

export default Model;
