import React, { FC, useEffect } from 'react';
import {
  Card,
  List,
  Descriptions,
} from 'antd';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect, Dispatch, IPostClaimFormParam } from 'umi';
import { ClaimFormChemicalModelState } from './model';
import styles from './style.less';
import { IChemical } from '@/models/entity';

interface ChemicalListProps {
  claimDetail?: IPostClaimFormParam;
  formid: number;
  dispatch: Dispatch;
  loading: boolean;
}
/*
const Info: FC<{
  title: React.ReactNode;
  value: React.ReactNode;
  bordered?: boolean;
}> = ({ title, value, bordered }) => (
  <div className={styles.headerInfo}>
    <span>{title}</span>
    <p>{value}</p>
    {bordered && <em />}
  </div>
);
*/
const ListContent = ({
  claimDetail: { state },
}: {
  claimDetail: IChemical;
}) => (
    <div className={styles.listContent}>
      <div className={styles.listContentItem}>
        <span>在库状态</span>
        <p>{state}</p>
      </div>
    </div>
  );

export const ChemicalList: FC<ChemicalListProps> = (props) => {

  const {
    loading,
    dispatch,
    claimDetail,
    formid
  } = props;
  useEffect(() => {
    dispatch({
      type: 'claimFormChemical/fetch',
      payload: {
        formid: formid
      }
    });
  }, [formid]);
  const paginationProps = {
    showQuickJumper: true,
    pageSize: 5,
    total: claimDetail?.chemicals.length,
  };

  return (
    <div>
      <PageHeaderWrapper>

        <Card style={{ marginBottom: 24 }} bordered={false} title={'表单'} >

          <Descriptions bordered>
            <Descriptions.Item label="申请编号">{claimDetail?.form.id}</Descriptions.Item>
            <Descriptions.Item label="所属实验室编号">{claimDetail?.form.lid}</Descriptions.Item>
            <Descriptions.Item label="提交时间">{claimDetail?.form.stime}</Descriptions.Item>
            <Descriptions.Item label="申请人姓名">{claimDetail?.form.uname}</Descriptions.Item>
            <Descriptions.Item label="预计归还时间">{claimDetail?.form.rtime}</Descriptions.Item>
            <Descriptions.Item label="当前状态">{claimDetail?.form.state}</Descriptions.Item>
          </Descriptions>
        </Card>
        <div className={styles.standardList}>
          <Card
            className={styles.listCard}
            bordered={false}
            title="基本列表"
            style={{ marginTop: 24 }}
            bodyStyle={{ padding: '0 32px 40px 32px' }}
          >
            <List
              size="large"
              rowKey="id"
              loading={loading}
              pagination={paginationProps}
              dataSource={claimDetail?.chemicals}
              renderItem={(item) => (
                <List.Item>
                  <List.Item.Meta
                    title={<span>{item.name}</span>}
                    description={<div>
                      <span>{item.amount}</span>
                      <span>{item.unitmeasurement}</span>
                    </div>}
                  />
                  <ListContent claimDetail={item} />
                </List.Item>
              )}
            />
          </Card>
        </div>
      </PageHeaderWrapper>

    </div>
  );
};

export default connect(
  ({
    claimFormChemical,
    loading,
  }: {
    claimFormChemical: ClaimFormChemicalModelState;
    loading: {
      models: { [key: string]: boolean };
    };
  }, ownProps: any) => ({
    formid: ownProps.match.params.formid,
    claimDetail: claimFormChemical.data,
    loading: loading.models.chemicalList,
  })
)(ChemicalList);
