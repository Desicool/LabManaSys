import { Effect, Reducer, IPostClaimFormParam } from 'umi';
import { queryClaimDetail, approveClaim, rejectClaim } from './service';
import { message } from 'antd';

export interface ClaimProcessModelState {
    data?: IPostClaimFormParam;
}

export interface ClaimProcessModelType {
    namespace: string;
    state: ClaimProcessModelState;
    effects: {
        fetch: Effect;
        approve: Effect;
        reject: Effect;
    };
    reducers: {
        fetchSuccess: Reducer<ClaimProcessModelState>;
    };
}

const Model: ClaimProcessModelType = {
    namespace: 'claimProcess',

    state: {
    },

    effects: {
        *fetch({ payload }, { call, put }) {
            const response = yield call(queryClaimDetail, payload.formid);
            yield put({
                type: 'fetchSuccess',
                payload: response,
            });
        },
        *approve({ payload }, { call }) {
            yield call(approveClaim, payload);
            message.success('提交成功');
        },
        *reject({ payload }, { call }) {
            yield call(rejectClaim, payload);
            message.success('提交成功');
        }
    },

    reducers: {
        fetchSuccess(state, action) {
            return {
                ...state,
                data: action.payload,
            };
        },
    },
};

export default Model;
