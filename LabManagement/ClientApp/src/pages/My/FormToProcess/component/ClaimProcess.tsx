import React, { FC } from 'react';
import { connect, FormToProcessState, history } from 'umi';
import { IClaimForm } from '@/models/entity';
import { Table } from 'antd';

interface ClaimProcessProps {
    claims: IClaimForm[];
    loading: boolean;
}
const FinancialProcessComponent: FC<ClaimProcessProps> = (props) => {
    const { claims } = props;
    const dataSource = claims.map(u => ({ ...u, key: u.id }));

    const columns = [
        {
            title: '编号',
            dataIndex: 'id',
            width: '4em'
        },
        {
            title:'申请人',
            dataIndex: 'uname',
            width: '8em'
        },
        {
            title: '预计归还时间',
            dataIndex: 'rtime'
        },
         {
            title: '进行处理',
            key: 'operation',
            render: () => <a onClick={()=>history.push('/my/process/financial')}>查看详情</a>,
        },
    ];
    return <div><Table dataSource={dataSource} columns={columns} />
    </div>
}

export default connect(
    ({
        myFormToProcess,
        loading,
    }: {
        myFormToProcess: FormToProcessState;
        loading: {
            effects: { [key: string]: boolean };
        };
    }) => ({
        claims: myFormToProcess.msg?.cform || [],
        loading: loading.effects['myFormToProcess/fetchAdvanced'],
    }),
)(FinancialProcessComponent);
