import { Effect, Reducer } from 'umi';

import { IFinancialForm, IDeclarationForm, IWorkFlow } from '@/models/entity';
import { getFinancialForms, getDeclarationForms, getWorkFlowById } from './service';

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
            const declaration = yield call(getDeclarationForms, payload);
            const financial = yield call(getFinancialForms, payload);
            yield put({
                type: 'fetchWorkFlowSuccess',
                payload: {
                    currentWorkFlow: workflow,
                    declarationForm: declaration,
                    financialForms: Array.isArray(financial) ? financial : [],
                }
            })
        }
    },

    reducers: {
        fetchWorkFlowSuccess(state = initState, action) {
            console.log(action);
            return {
                ...state,
                ...action.payload
            }
        }
    },
};

export default Model;
